--[[
    GD50
    Legend of Zelda

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

Projectile = Class{__includes = GameObject}

function Projectile:enter(params)
    
end

-- called from Entity
function Projectile:moveMent(player)
    self.state = 'picked'
    if player.direction == 'up' then
        self.direction = player.direction
        Timer.tween(0.05, {
            [self] = {x = player.x - 6 , y = player.y - 12}
        })

    elseif player.direction == 'down' then
        self.direction = player.direction
        Timer.tween(0.05, {
            [self] = {x = player.x - 6 , y = player.y - 8}
        })

    elseif player.direction == 'right' then
        self.direction = player.direction
        Timer.tween(0.05, {
            [self] = {x = player.x - 4 , y = player.y - 10}
        })

    elseif player.direction == 'left' then
        self.direction = player.direction
        Timer.tween(0.05, {
            [self] = {x = player.x - 6, y = player.y - 10}
        })
    end
end

-- called from Entity
function Projectile:ObjectThrownMovement(player, dt)
    self.state = 'picked'
    if self.bump == false then
        if self.direction == 'up' then
            self.y = self.y - (PROJECTILE_SPEED * dt)


            -- Wall Colision Check
            if self.y <= MAP_RENDER_OFFSET_Y + TILE_SIZE - self.height / 2 then 
                self.y = MAP_RENDER_OFFSET_Y + TILE_SIZE - self.height / 2
                self.bump = true
            end

            -- Traveled Y Axis
            
            self.TraveledY = self.TraveledY + 1
            if self.TraveledY == TILE_SIZE then
                self.tilesTraveled = self.tilesTraveled + 1
                self.TraveledY = 0
            end

        elseif self.direction == 'down' then
            self.y = self.y + (PROJECTILE_SPEED * dt)

            -- Wall Colision Check
            local bottomEdge = VIRTUAL_HEIGHT - (VIRTUAL_HEIGHT - MAP_HEIGHT * TILE_SIZE) 
            + MAP_RENDER_OFFSET_Y - TILE_SIZE

            if self.y + self.height >= bottomEdge then
                self.y = bottomEdge - self.height
                self.bump = true
            end
            
            -- Traveled Y Axis
            self.TraveledY = self.TraveledY + 0.3
            if math.floor(self.TraveledY) == TILE_SIZE then
                self.tilesTraveled = self.tilesTraveled + 1
                self.TraveledY = 0
            end

        elseif self.direction == 'right' then
            self.x = self.x + ( (TILE_SIZE + PROJECTILE_SPEED ) * dt)
            -- Wall Colision Check
            if self.x + self.width >= VIRTUAL_WIDTH - TILE_SIZE * 2 then
                self.x = VIRTUAL_WIDTH - TILE_SIZE * 2 - self.width
                self.bump = true
            end

            -- Traveled X Axis
            self.TraveledX = self.TraveledX + 0.1
            if math.floor(self.TraveledX) == TILE_SIZE then
                self.tilesTraveled = self.TraveledX + 1
                self.TraveledX = 0
            end

        elseif self.direction == 'left' then
            self.x = self.x - (PROJECTILE_SPEED * dt)

            -- Wall Colision Check
            if self.x <= MAP_RENDER_OFFSET_X + TILE_SIZE then
                self.x = MAP_RENDER_OFFSET_X + TILE_SIZE
                self.bump = true
            end

            -- Traveled X Axis
            self.TraveledX = self.TraveledX + 0.1
            if math.floor(self.TraveledX) == TILE_SIZE then
                self.tilesTraveled = self.TraveledX + 1
                self.TraveledX = 0
            end
        end
    end

    if self.bump then
        self.state = 'broken'
        Timer.tween(4, {
            [self] = {opacity = 0}
        }):finish(function () 
            player.carriedObject = nil
            player.thrown = false
        end)
        --player.carriedObject = nil
        --player.thrown = false
    end
end

-- Called in Room
function Projectile:CheckTilesCrossed(player, tiles)
    if tiles ~= nil then
        if self.tilesTraveled > 4 then
            self.bump = true
            self.tilesTraveled = 0
        end
    end
end