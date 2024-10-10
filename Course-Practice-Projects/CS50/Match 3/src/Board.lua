--[[
    GD50
    Match-3 Remake

    -- Board Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu

    The Board is our arrangement of Tiles with which we must try to find matching
    sets of three horizontally or vertically.
]]

Board = Class{}

function Board:init(x, y, level)
    self.x = x
    self.y = y
    self.matches = {}

    self:initializeTiles(level)
end

function Board:initializeTiles(level)
    self.tiles = {}

    for tileY = 1, 8 do
        
        -- empty table that will serve as a new row
        table.insert(self.tiles, {})

        if level == 1 then
            for tileX = 1, 8 do
                
                local ShinyCheck = math.random(1,50)
                -- create a new tile at X,Y with a random color and variety
                table.insert(self.tiles[tileY], Tile(tileX, tileY, math.random(8), 1, ShinyCheck == 25 and true or false))
            end
        elseif level ~= 1 then
            for tileX = 1, 8 do

                local ShinyCheck = math.random(1,50)
                -- create a new tile at X,Y with a random color and variety
                table.insert(self.tiles[tileY], Tile(tileX, tileY, math.random(8), math.random(6), ShinyCheck == 7 and true or false))
            end
        end
    end

    while self:calculateMatches() do
        
        -- recursively initialize if matches were returned so we always have
        -- a matchless board on start
        self:initializeTiles(level)
    end

    while not self:AvaialableMatches() do
        self:initializeTiles(level)
    end
end

--[[
    Goes left to right, top to bottom in the board, calculating matches by counting consecutive
    tiles of the same color. Doesn't need to check the last tile in every row or column if the 
    last two haven't been a match.
]]
function Board:calculateMatches()
    local matches = {}

    local ShinyFlag = false
    -- how many of the same color blocks in a row we've found
    local matchNum = 1

    -- horizontal matches first
    for y = 1, 8 do
        local colorToMatch = self.tiles[y][1].color
        ShinyFlag = false
        matchNum = 1
        
        -- every horizontal tile
        for x = 2, 8 do
            if self.tiles[y][x].Shiny == true then
                ShinyFlag = true
            end
            -- if this is the same color as the one we're trying to match...
            if self.tiles[y][x].color == colorToMatch then
                matchNum = matchNum + 1
            else
                
                -- set this as the new color we want to watch for
                colorToMatch = self.tiles[y][x].color

                -- if we have a match of 3 or more up to now, add it to our matches table
                if matchNum >= 3 then
                    local match = {}
                    if ShinyFlag then
                        matchNum = 8
                        x = 9
                    end

                    -- go backwards from here by matchNum
                    for x2 = x - 1, x - matchNum, -1 do
                        
                        -- add each tile to the match that's in that match
                        table.insert(match, self.tiles[y][x2])
                    end

                    -- add this match to our total matches table
                    table.insert(matches, match)
                end

                matchNum = 1

                -- don't need to check last two if they won't be in a match
                --if x >= 7 then
                --    break
                --end
            end
        end

        -- account for the last row ending with a match
        if matchNum >= 3 then
            local match = {}
            if self.tiles[y][8].Shiny == true then
                ShinyFlag = true
                matchNum = 8
            elseif self.tiles[y][7].Shiny == true then
                ShinyFlag = true
                matchNum = 8
            end
            -- go backwards from end of last row by matchNum
            for x = 8, 8 - matchNum + 1, -1 do
                table.insert(match, self.tiles[y][x])
            end

            table.insert(matches, match)
        end
    end

    -- vertical matches
    for x = 1, 8 do
        local colorToMatch = self.tiles[1][x].color
        ShinyFlag = false
        matchNum = 1

        -- every vertical tile
        for y = 2, 8 do
            if self.tiles[y][x].Shiny == true then
                ShinyFlag = true
            end
            
            if self.tiles[y][x].color == colorToMatch then
                matchNum = matchNum + 1
            else
                colorToMatch = self.tiles[y][x].color

                if matchNum >= 3 then
                    local match = {}
                    if ShinyFlag then
                        y = 9
                    end

                    for y2 = y - 1, y - matchNum, -1 do
                        table.insert(match, self.tiles[y2][x])
                    end
                end

                matchNum = 1

                -- don't need to check last two if they won't be in a match
                if y >= 7 then
                    break
                end
            end
        end

        -- account for the last column ending with a match
        if matchNum >= 3 then
            local match = {}
            if self.tiles[8][x].Shiny == true then
                ShinyFlag = true
                y = 8
            elseif self.tiles[7][x].Shiny == true then
                ShinyFlag = true
                y = 8
            end
            -- go backwards from end of last row by matchNum
            for y = 8, 8 - matchNum + 1, -1 do
                table.insert(match, self.tiles[y][x])
            end

            table.insert(matches, match)
        end
    end

    -- store matches for later reference
    self.matches = matches

    -- return matches table if > 0, else just return false
    return #self.matches > 0 and self.matches or false
end

--[[
    Remove the matches from the Board by just setting the Tile slots within
    them to nil, then setting self.matches to nil.
]]
function Board:removeMatches()
    for k, match in pairs(self.matches) do
        for k, tile in pairs(match) do
            self.tiles[tile.gridY][tile.gridX] = nil
        end
    end

    self.matches = nil
end

--[[
    Shifts down all of the tiles that now have spaces below them, then returns a table that
    contains tweening information for these new tiles.
]]
function Board:getFallingTiles(level)
    -- tween table, with tiles as keys and their x and y as the to values
    local tweens = {}

    -- for each column, go up tile by tile till we hit a space
    for x = 1, 8 do
        local space = false
        local spaceY = 0

        local y = 8
        while y >= 1 do
            
            -- if our last tile was a space...
            local tile = self.tiles[y][x]
            
            if space then
                
                -- if the current tile is *not* a space, bring this down to the lowest space
                if tile then
                    
                    -- put the tile in the correct spot in the board and fix its grid positions
                    self.tiles[spaceY][x] = tile
                    tile.gridY = spaceY

                    -- set its prior position to nil
                    self.tiles[y][x] = nil

                    -- tween the Y position to 32 x its grid position
                    tweens[tile] = {
                        y = (tile.gridY - 1) * 32
                    }

                    -- set Y to spaceY so we start back from here again
                    space = false
                    y = spaceY

                    -- set this back to 0 so we know we don't have an active space
                    spaceY = 0
                end
            elseif tile == nil then
                space = true
                
                -- if we haven't assigned a space yet, set this to it
                if spaceY == 0 then
                    spaceY = y
                end
            end

            y = y - 1
        end
    end

    -- create replacement tiles at the top of the screen
    for x = 1, 8 do
        for y = 8, 1, -1 do
            local tile = self.tiles[y][x]

            -- if the tile is nil, we need to add a new one
            if level == 1 then

                if not tile then
                    -- new tile with random color and variety
                    local ShinyCheck = math.random(1,50)
                    local tile = Tile(x, y, math.random(8), 1, ShinyCheck == 25 and true or false)
                    tile.y = -32
                    self.tiles[y][x] = tile

                    -- create a new tween to return for this tile to fall down
                    tweens[tile] = {
                        y = (tile.gridY - 1) * 32
                    }
                end
            elseif level ~= 1 then
                if not tile then
                    -- new tile with random color and variety
                    local ShinyCheck = math.random(1,50)
                    local tile = Tile(x, y, math.random(8), math.random(6), ShinyCheck == 25 and true or false)
                    tile.y = -32
                    self.tiles[y][x] = tile

                    -- create a new tween to return for this tile to fall down
                    tweens[tile] = {
                        y = (tile.gridY - 1) * 32
                    }
                end
            end
        end
    end

    return tweens
end

function Board:render()
    for y = 1, #self.tiles do
        for x = 1, #self.tiles[1] do
            self.tiles[y][x]:render(self.x, self.y)
        end
    end
end

-- Function to Check Whether Matches are Avaialable or not
function Board:AvaialableMatches()
    local possibleMatches = {}

    -- every Vertical Tile
    for y = 1, 8 do
        -- every horizontal tile
        for x = 1, 8 do
            local tile = self.tiles[y][x]

            -- Check Swapping with the Title Above
            if y > 1 then 
                local Tile1 = self.tiles[y][x]
                local Tile2 = self.tiles[y - 1][x]
                self:SwapTiles(Tile1, Tile2)
                if self:calculateMatches() then
                    table.insert(possibleMatches, {Tile1,Tile2})
                end
                -- Swap to Orignal values
                self:SwapTiles(Tile1, Tile2)
            end

            -- Check Swapping with Tile Below
            if y < 8 then 
                local Tile1 = self.tiles[y][x]
                local Tile2 = self.tiles[y + 1][x]
                self:SwapTiles(Tile1, Tile2)
                if self:calculateMatches() then
                    table.insert(possibleMatches, {Tile1,Tile2})
                end
                -- Swap to Orignal values
                self:SwapTiles(Tile1, Tile2)
            end

            --Check Swapping with Tile to the Left
            if x > 1 then 
                local Tile1 = self.tiles[y][x]
                local Tile2 = self.tiles[y][x - 1]
                self:SwapTiles(Tile1, Tile2)
                if self:calculateMatches() then
                    table.insert(possibleMatches, {Tile1,Tile2})
                end
                -- Swap to Orignal values
                self:SwapTiles(Tile1, Tile2)
            end
            
            --Check Swapping with Tile to the Right
            if x < 8 then 
                local Tile1 = self.tiles[y][x]
                local Tile2 = self.tiles[y][x + 1]
                self:SwapTiles(Tile1, Tile2)
                if self:calculateMatches() then
                    table.insert(possibleMatches, {Tile1,Tile2})
                end
                -- Swap to Orignal values
                self:SwapTiles(Tile1, Tile2)
            end
        end
    end


    -- return matches table if > 0, else just return false
    return #possibleMatches > 0 and possibleMatches or false
end

--Function To Swap Tiles in the Board
function Board:SwapTiles(Tile1, Tile2)
    local tempX = Tile1.gridX
    local tempY = Tile1.gridY

    local newTile = Tile2

    Tile1.gridX = newTile.gridX
    Tile1.gridY = newTile.gridY
    newTile.gridX = tempX
                
    newTile.gridY = tempY

    -- swap tiles in the tiles table
    self.tiles[Tile1.gridY][Tile1.gridX] = Tile1
    self.tiles[newTile.gridY][newTile.gridX] = newTile
end