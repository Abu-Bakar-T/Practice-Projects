--[[
    GD50
    Angry Birds

    Author: Colton Ogden
    cogden@cs50.harvard.edu
]]

AlienLaunchMarker = Class{}

function AlienLaunchMarker:init(world)
    self.world = world

    -- starting coordinates for launcher used to calculate launch vector
    self.baseX = 90
    self.baseY = VIRTUAL_HEIGHT - 100

    -- shifted coordinates when clicking and dragging launch alien
    self.shiftedX = self.baseX
    self.shiftedY = self.baseY

    -- whether our arrow is showing where we're aiming
    self.aiming = false

    -- whether we launched the alien and should stop rendering the preview
    self.launched = false

    -- whether we have collided or not
    self.collision = false

    -- our alien we will eventually spawn
    self.alien = {}
end

function AlienLaunchMarker:update(dt)

    --for i = #self.obstacles, 1, -1 do
    --    if self.obstacles[i].body:isDestroyed() then
    --        table.remove(self.obstacles, i)
    --    end
    --end
    
    -- perform everything here as long as we haven't launched yet
    if not self.launched then

        local alien

        -- grab mouse coordinates
        local x, y = push:toGame(love.mouse.getPosition())
        
        -- if we click the mouse and haven't launched, show arrow preview
        if love.mouse.wasPressed(1) and not self.launched then
            self.aiming = true

        -- if we release the mouse, launch an Alien
        elseif love.mouse.wasReleased(1) and self.aiming then
            self.launched = true

            -- spawn new alien in the world, passing in user data of player
            alien = Alien(self.world, 'round', self.shiftedX, self.shiftedY, 'Player')

            -- apply the difference between current X,Y and base X,Y as launch vector impulse
            alien.body:setLinearVelocity((self.baseX - self.shiftedX) * 10, (self.baseY - self.shiftedY) * 10)

            -- make the alien pretty bouncy
            alien.fixture:setRestitution(0.4)
            alien.body:setAngularDamping(1)

            -- we're no longer aiming
            self.aiming = false

        -- re-render trajectory
        elseif self.aiming then
            
            self.shiftedX = math.min(self.baseX + 30, math.max(x, self.baseX - 30))
            self.shiftedY = math.min(self.baseY + 30, math.max(y, self.baseY - 30))
        end

        table.insert(self.alien,alien)
    end

    if self.launched and self.collision == false and love.keyboard.keysPressed['space'] then
        local alien1    -- Alien 1
        local alien2    -- Alien 2
        local alienx = self.alien[1].body:getX()
        local alieny = self.alien[1].body:getY()
        local alienXvelocity, alienYvelocity = self.alien[1].body:getLinearVelocity()

        -- Alien 1 is above
        -- spawn new alien in the world, passing in user data of player
        alien1 = Alien(self.world, 'round', alienx, alieny - 1, 'Player')
        -- apply the difference between current X,Y and base X,Y as launch vector impulse
        alien1.body:setLinearVelocity(alienXvelocity, alienYvelocity - 10)
        -- make the alien pretty bouncy
        alien1.fixture:setRestitution(0.4)
        alien1.body:setAngularDamping(1)
        table.insert(self.alien,alien1)

        -- Alien 2 is below
        -- spawn new alien in the world, passing in user data of player
        alien2 = Alien(self.world, 'round', alienx, alieny + 1, 'Player')
        -- apply the difference between current X,Y and base X,Y as launch vector impulse
        alien2.body:setLinearVelocity(alienXvelocity, alienYvelocity + 10)
        -- make the alien pretty bouncy
        alien2.fixture:setRestitution(0.4)
        alien2.body:setAngularDamping(1)
        table.insert(self.alien,alien2)
    end
end

function AlienLaunchMarker:render()
    if not self.launched then
        
        -- render base alien, non physics based
        love.graphics.draw(gTextures['aliens'], gFrames['aliens'][9], 
            self.shiftedX - 17.5, self.shiftedY - 17.5)

        if self.aiming then
            
            -- render arrow if we're aiming, with transparency based on slingshot distance
            local impulseX = (self.baseX - self.shiftedX) * 10
            local impulseY = (self.baseY - self.shiftedY) * 10

            -- draw 18 circles simulating trajectory of estimated impulse
            local trajX, trajY = self.shiftedX, self.shiftedY
            local gravX, gravY = self.world:getGravity()

            -- http://www.iforce2d.net/b2dtut/projected-trajectory
            for i = 1, 90 do
                
                -- magenta color that starts off slightly transparent
                love.graphics.setColor(255/255, 80/255, 255/255, ((255 / 24) * i) / 255)
                
                -- trajectory X and Y for this iteration of the simulation
                trajX = self.shiftedX + i * 1/60 * impulseX
                trajY = self.shiftedY + i * 1/60 * impulseY + 0.5 * (i * i + i) * gravY * 1/60 * 1/60

                -- render every fifth calculation as a circle
                if i % 5 == 0 then
                    love.graphics.circle('fill', trajX, trajY, 3)
                end
            end
        end
        
        love.graphics.setColor(1, 1, 1, 1)
    else
        for i = #self.alien, 1, -1 do
            self.alien[i]:render()
        end
    end
end