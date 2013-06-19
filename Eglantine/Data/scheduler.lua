---------------------
----scheduler.lua----
---------------------

--Stores all running couroutines
local WAITING_COROUTINES = {}

--The current game time.
local CURRENT_TIME = 0



function waitSeconds(sec)
	--Grab the current running coroutine
	local this_routine = coroutine.running()
	if(this_routine ~= nil) then
		--Find the time to resume the coroutine
		local resumeTime = CURRENT_TIME + sec
		
		--Store the coroutine and resume time in the table
		WAITING_COROUTINES[this_routine] = resumeTime
		--Suspend
		return coroutine.yield(this_routine)
	end
end
	
function updateCoroutines(deltaTime)
	--Update current time
	CURRENT_TIME = CURRENT_TIME + deltaTime
	
	--Create a list of coroutines to awaken
	local resumeList = {}
	
	--Check each suspended routine to see if it should resume this frame
	for co, t in pairs(WAITING_COROUTINES) do
		print(CURRENT_TIME.." :: "..t)
		if(CURRENT_TIME >= t) then
			table.insert(resumeList, co)
		end
	end
	
	--Awake the resumeList
	for i, co in ipairs(resumeList) do
		--Remove the coroutine from the table
		WAITING_COROUTINES[co] = nil
		--Resume the coroutine
		coroutine.resume(co)
	end
end

function runCoroutine(func)
	local co = coroutine.create(func)
	return coroutine.resume(co)
end