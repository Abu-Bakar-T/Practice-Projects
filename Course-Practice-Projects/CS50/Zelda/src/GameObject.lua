--[[
    GD50
    Legend of Zelda

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

GameObject = Class{}

function GameObject:init(def, x, y)
    
    -- string identifying this object type
    self.type = def.type

    self.texture = def.texture
    self.frame = def.frame or 1

    -- whether it acts as an obstacle or not
    self.solid = def.solid
    self.direction = def.direction
    self.consumeable = def.consumeable
    self.consumed = def.consumed
    self.pickable = def.pickable
    self.collidable = def.collidable
    self.bump = def.bump
    self.opacity = def.opactiy or 255

    self.defaultState = def.defaultState
    self.state = self.defaultState
    self.states = def.states

    self.tilesTraveled = 0
    self.TraveledX = 0
    self.TraveledY = 0
    self.Oldx = 0
    self.Oldy = 0

    -- dimensions
    self.x = x
    self.y = y
    self.width = def.width
    self.height = def.height
    self.dx = 1
    self.dy = 1

    -- default empty collision callback
    self.onCollide = function(obj) end
end

function GameObject:update(dt)

end

function GameObject:collides(target)
    local selfY, selfHeight = self.y + self.height / 2, self.height - self.height / 2
    
    return not (self.x + self.width < target.x or self.x > target.x + target.width or
                selfY + selfHeight < target.y or selfY > target.y + target.height)
end

function GameObject:render(adjacentOffsetX, adjacentOffsetY)
    love.graphics.setColor(1,1,1,self.opacity/255)
    love.graphics.draw(gTextures[self.texture], gFrames[self.texture][self.states[self.state].frame or self.frame],
        self.x + adjacentOffsetX, self.y + adjacentOffsetY)

    -- For Debug
    -- love.graphics.setColor(255, 0, 255, 255)
    -- love.graphics.rectangle('line', self.x, self.y, self.width, self.height)
    -- love.graphics.setColor(255, 255, 255, 255)
end