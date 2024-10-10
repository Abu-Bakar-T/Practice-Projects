--[[
    GD50
    Super Mario Bros. Remake

    -- LevelMaker Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

LevelMaker = Class{}

function LevelMaker.generate(Twidth, height)
    local random_lock_key_color = math.random(1,4)
    local tiles = {}
    local entities = {}
    local objects = {}
    local LockedBoxes = {}

    local key_Obtained = false
    local VictoryflagSpawn = true
    
    local tileID = TILE_ID_GROUND
    
    -- whether we should draw our tiles with toppers
    local topper = true
    local tileset = math.random(20)
    local topperset = math.random(20)

    -- insert blank tables into tiles for later access
    for x = 1, height do
        table.insert(tiles, {})
    end

    -- column by column generation instead of row; sometimes better for platformers
    for x = 1, Twidth do
        local tileID = TILE_ID_EMPTY
        
        -- lay out the empty space
        for y = 1, 6 do
            table.insert(tiles[y],
                Tile(x, y, tileID, nil, tileset, topperset))
        end

        -- chance to just be emptiness
        if math.random(7) == 1 then
            for y = 7, height do
                table.insert(tiles[y],
                    Tile(x, y, tileID, nil, tileset, topperset))
            end
        else
            tileID = TILE_ID_GROUND

            -- height at which we would spawn a potential jump block
            local blockHeight = 4

            for y = 7, height do
                table.insert(tiles[y],
                    Tile(x, y, tileID, y == 7 and topper or nil, tileset, topperset))
            end

            -- chance to generate a pillar
            if math.random(8) == 1 then
                blockHeight = 2
                
                -- chance to generate bush on pillar
                if math.random(8) == 1 then
                    table.insert(objects,
                        GameObject {
                            texture = 'bushes',
                            x = (x - 1) * TILE_SIZE,
                            y = (4 - 1) * TILE_SIZE,
                            width = 16,
                            height = 16,
                            
                            -- select random frame from bush_ids whitelist, then random row for variance
                            frame = BUSH_IDS[math.random(#BUSH_IDS)] + (math.random(4) - 1) * 7,
                            collidable = false
                        }
                    )
                end
                
                -- pillar tiles
                tiles[5][x] = Tile(x, 5, tileID, topper, tileset, topperset)
                tiles[6][x] = Tile(x, 6, tileID, nil, tileset, topperset)
                tiles[7][x].topper = nil
            
            -- chance to generate bushes
            elseif math.random(8) == 1 then
                table.insert(objects,
                    GameObject {
                        texture = 'bushes',
                        x = (x - 1) * TILE_SIZE,
                        y = (6 - 1) * TILE_SIZE,
                        width = 16,
                        height = 16,
                        frame = BUSH_IDS[math.random(#BUSH_IDS)] + (math.random(4) - 1) * 7,
                        collidable = false
                    }
                )

            -- chance to spawn a block
            elseif math.random(5) == 1 then
                table.insert(objects,

                    -- jump block
                    GameObject {
                        texture = 'jump-blocks',
                        x = (x - 1) * TILE_SIZE,
                        y = (blockHeight - 1) * TILE_SIZE,
                        width = 16,
                        height = 16,

                        -- make it a random variant
                        frame = math.random(#JUMP_BLOCKS),
                        collidable = true,
                        hit = false,
                        solid = true,

                        -- collision function takes itself
                        onCollide = function(obj)

                            -- spawn a gem if we haven't already hit the block
                            if not obj.hit then

                                -- chance to spawn gem, not guaranteed
                                if math.random(5) == 1 then

                                    -- maintain reference so we can set it to nil
                                    local gem = GameObject {
                                        texture = 'gems',
                                        x = (x - 1) * TILE_SIZE,
                                        y = (blockHeight - 1) * TILE_SIZE - 4,
                                        width = 16,
                                        height = 16,
                                        frame = math.random(#GEMS),
                                        collidable = true,
                                        consumable = true,
                                        solid = false,

                                        -- gem has its own function to add to the player's score
                                        onConsume = function(player, object)
                                            gSounds['pickup']:play()
                                            player.score = player.score + 100
                                        end
                                    }
                                    
                                    -- make the gem move up from the block and play a sound
                                    Timer.tween(0.1, {
                                        [gem] = {y = (blockHeight - 2) * TILE_SIZE}
                                    })
                                    gSounds['powerup-reveal']:play()

                                    table.insert(objects, gem)
                                end

                                obj.hit = true
                            end

                            gSounds['empty-block']:play()
                        end
                    }
                )
            -- chance to spawn Lock Block
            elseif math.random(10) == 1 then
                

                -- Locked block
                table.insert(objects,

                    -- Locked block
                    GameObject {
                        texture = 'keys-and-locks',
                        x = (x - 1) * TILE_SIZE,
                        y = (blockHeight - 1) * TILE_SIZE,
                        width = 16,
                        height = 16,
                        -- make it a random variant
                        frame = random_lock_key_color + 4,
                        collidable = true,
                        consumable = false,
                        hit = false,
                        solid = true,
                        KeyObtained = false,
                    
                        -- collision function takes itself
                        onCollide = function(obj)
                            obj.hit = true
                            if key_Obtained then
                                obj.solid = false
                                obj.keyObtained = true
                                obj.consumable = true
                            end

                            if key_Obtained and VictoryflagSpawn then

                                -- Spawn poles
                                table.insert(objects,
                                    GameObject {
                                        texture = 'poles',
                                        x = (Twidth - 2) * TILE_SIZE,
                                        y = (blockHeight - 1) * TILE_SIZE,
                                        width = 16,
                                        height = 32,
                                        -- make it a random variant
                                        frame = random_lock_key_color,
                                        collidable = false,
                                        consumable = true,
                                        solid = false,
                                        
                                        onConsume = function(player, object)
                                            gStateMachine:change('play',{ map = LevelMaker.generate(Twidth + 50, height),
                                                background = math.random(3),
                                                score = player.score
                                            })
                                            gSounds['pickup']:play()
                                        end      
                                    }
                                )

                                -- Spawn 1st flag
                                table.insert(objects,
                                    GameObject {
                                        texture = 'flags',
                                        x = (Twidth - 2) * TILE_SIZE + 6,
                                        y = (blockHeight - 1) * TILE_SIZE,
                                        width = 16,
                                        height = 16,
                                        -- make it a random variant
                                        frame = random_lock_key_color,
                                        collidable = false,
                                        consumable = true,
                                        solid = false,
                                        
                                        onConsume = function(player, object)
                                            gStateMachine:change('play',{ map = LevelMaker.generate(Twidth + 50, height),
                                                background = math.random(3),
                                                score = player.score
                                            })
                                            gSounds['pickup']:play()
                                        end              
                                    }
                                )

                                -- Spawn 2nd flag
                                table.insert(objects,
                                    GameObject {
                                        texture = 'flags',
                                        x = (Twidth - 2) * TILE_SIZE + 6,
                                        y = (blockHeight) * TILE_SIZE + 1,
                                        width = 16,
                                        height = 16,
                                        -- make it a random variant
                                        frame = random_lock_key_color,
                                        collidable = false,
                                        consumable = true,
                                        solid = false,
                                        
                                        onConsume = function(player, object)
                                            gStateMachine:change('play',{ map = LevelMaker.generate(Twidth + 50, height),
                                                background = math.random(3),
                                                score = player.score
                                            })
                                            gSounds['pickup']:play()
                                        end             
                                    }
                                )

                                VictoryflagSpawn = false
                            end

                            gSounds['empty-block']:play()
                        end,               
                        
                        onConsume = function(player, object)


                            gSounds['pickup']:play()
                        end                     
                    }
                )

            -- chance to Key
            elseif math.random(10) == 1 then
                

                -- Key
                table.insert(objects,

                    -- jump block
                    GameObject {
                        texture = 'keys-and-locks',
                        x = (x - 1) * TILE_SIZE,
                        y = (blockHeight - 1) * TILE_SIZE,
                        width = 16,
                        height = 16,
                        -- make it a random variant
                        frame = random_lock_key_color,
                        collidable = true,
                        consumable = true,
                        solid = false,
                        
                        onConsume = function(player, object)
                            gSounds['pickup']:play()
                            key_Obtained = true
                        end            
                    }
                )
                gSounds['powerup-reveal']:play()
            
            end
        end
    end

    local map = TileMap(Twidth, height)
    map.tiles = tiles
    
    return GameLevel(entities, objects, map)
end