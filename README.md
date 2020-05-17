# Unity Quaternion -> Euler Converter

Unit tests for checking Unity can control the movement of a 2DOF 360-capable pitch & roll flight simulator in a way that makes sense

## Test Table
If I do this in the spaceship… | Then I expect these Euler values returned…
------------ | -------------
Only pitch rotation – spaceship “backflips” |	Only pitch value changes, 0 – roll, 0 -yaw
Only roll rotation – “barrel roll” |	0 – pitch, roll values 0-360, 0-yaw
Only yaw rotation – “heading changes” |	0 – pitch, 0- roll, don’t care if yaw is “right” or not
•	Pitch +90 degrees “straight up”
•	Then pitch +90 to “back flip” |	180 pitch and no other movements, roll -0, yaw-0
•	Roll +90 degrees “left turn”
•	Then pitch up 90, 180,270,360 degrees	| 0 pitch, 90 roll, yaw moves to 90,180,270, 360
•	Pitch up +90 degrees “straight up”
•	Roll 90, 180,270,360 degrees |	90 – pitch, roll goes to 90, 180, 270, 360
•	Roll +90 degrees “bank left”
•	Yaw 90, 180, 270, 360 degrees	| Pitch goes to 90, 180, 270, 360, 90 – roll, 0-yaw
