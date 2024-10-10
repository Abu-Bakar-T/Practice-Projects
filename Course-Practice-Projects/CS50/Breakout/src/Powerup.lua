--[[
    GD50
    Breakout Remake

    -- PowerUp Class --

    Represents a Powerup that has key powerup for Locked Block and a powerup that spawns 2 more balls for a level
]]

PowerUp = Class{}

function PowerUp:init(skin)
    -- x is placed in the middle , 16 is self.width
    self.x = math.random(0+16,VIRTUAL_WIDTH-2 - 16)

    -- y is placed a little above the bottom edge of the screen
    self.y = 2

    -- start us off with no velocity
    self.dy = 0

    -- starting dimensions
    self.width = 16
    self.height = 16

    -- the skin only has the effect of changing our color, used to offset us
    -- into the gPowerUpSkins table later
    self.skin = skin

    self.inUse = true
end

function PowerUp:update(dt)
    if dt then 
    self.dy = self.dy + GRAVITY * dt
    end

    self.y = self.dy + self.y
end


function PowerUp:collides(target)
    -- first, check to see if the left edge of either is farther to the right
    -- than the right edge of the other
    if self.x > target.x + target.width or target.x > self.x + self.width then
        return false
    end

    -- then check to see if the bottom edge of either is higher than the top
    -- edge of the other
    if self.y > target.y + target.height or target.y > self.y + self.height then
        return false
    end 

    self.inUse = false
    -- if the above aren't true, they're overlapping
    return true
end

--[[
    Render the Powerup by drawing the main texture, passing in the quad
    that corresponds to the proper skin and size.
]]
function PowerUp:render()
    if self.inUse then
        love.graphics.draw(gTextures['main'], gFrames['powerups'][self.skin],
        self.x, self.y)
    end
end