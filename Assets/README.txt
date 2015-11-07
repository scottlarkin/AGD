The file "InputManager.asset" must be copied into the ProjectSettings folder.

Overwrite the exsisting file.

Movement script will not work otherwise.

Movement script has several prameters to play with-
	Acceleration rate - how fast the player speeds up
	Deceleraterion rate - how fast the player slows down
	Acceleration Gravity - how fast the player speeds up in the direction of gravity (negative y is down)
	Max Speed 
	Jump height - really the initial up velocity of the jump
	Numebr of jumps - Number of consecutive jumps before touching floor again

Notes:
	-Player rigid body should not use gravity as its programmed manually
	-Player rigid body should be frozen in z axis, and all rotation axis
	-Physics materials found in the materials folder should be attached to player and floors respectively
	-Players must be tagged "P_<player number>", eg- "P_1", "P_2", etc...
	
	
	