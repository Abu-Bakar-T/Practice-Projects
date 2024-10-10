PlayerPotLiftIdleState = Class{__includes = BaseState}

function PlayerPotLiftIdleState:init(player, dungeon)
    self.player = player
    self.dungeon = dungeon

    -- render offset for spaced character sprite
    self.player.offsetY = 5
    self.player.offsetX = 8

    self.player:changeAnimation('potliftIdle-' .. self.player.direction)
end

function PlayerPotLiftIdleState:enter(params)
    self.player.currentAnimation:refresh()
end

function PlayerPotLiftIdleState:update(dt)
    if self.player.carriedObject ~= nil then
        self.player.carriedObject.state = 'unpicked'
    end
    if love.keyboard.isDown('left') or love.keyboard.isDown('right') or
       love.keyboard.isDown('up') or love.keyboard.isDown('down') then
        self.player:changeState('pot-lift-walk')
    end

    if love.keyboard.wasPressed('space') then
        self.player.thrown = true
        self.player:ObjectThrown(dt)
        if love.keyboard.isDown('left') or love.keyboard.isDown('right') or
        love.keyboard.isDown('up') or love.keyboard.isDown('down') then
            self.player:changeState('walk')
        else
            self.player:changeState('idle')
        end
    end
end

function PlayerPotLiftIdleState:render()
    local anim = self.player.currentAnimation
    love.graphics.draw(gTextures[anim.texture], gFrames[anim.texture][anim:getCurrentFrame()],
        math.floor(self.player.x - self.player.offsetX), math.floor(self.player.y - self.player.offsetY))

    --
    -- debug for player and hurtbox collision rects VV
    --

    -- love.graphics.setColor(255, 0, 255, 255)
    -- love.graphics.rectangle('line', self.player.x, self.player.y, self.player.width, self.player.height)
    -- love.graphics.rectangle('line', self.swordHurtbox.x, self.swordHurtbox.y,
    --     self.swordHurtbox.width, self.swordHurtbox.height)
    -- love.graphics.setColor(255, 255, 255, 255)
end