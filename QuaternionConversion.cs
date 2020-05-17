//|---------------------------------------------------------------------------|
//|    FILE NAME: QuaternionConversion.cs                                     |
//|                                                                           |
//|    AUTHOR   : Alexander Redei                                             |
//|                                                                           |
//|    PURPOSE  : Converts Unity Quaternion rotations into Euler coordinates  |
//|                                                                           |
//|               Most important is the Pitch and Roll telemetry as those are |
//|                 what's primarily used by the flight simualtor. This class |
//|                 will prioritize those movements in a way that makes       |
//|					logical sense for the flight simulator.         		  |
//|                                                                           |
//|               This asset :                                                |
//|                 1) Accepts Quaternions as Inputs                          |
//|                 2) Generates Euler Coordinates as Outputs                 |
//|                                                                           |
//|    NOTES    : This is not a Unity Monobehavior.                           |
//|                 This class should not directly imported into the scene.   |
//|                                                                           |
//|    REVISIONS:                                                             |
//|			   5/15/20 - Alex R. - Created, getPitch, getRoll, getYaw added   |
//|			   5/15/20 - Alex R. - Tried 12 different conversion algorithms   |
//|			   5/16/20 - Alex R. - Tried an additional algorithms             |
//|---------------------------------------------------------------------------|
using UnityEngine;

//|---------------------------------------------------------------------------|
//|    CLASS    : QuaternionConversion                                        |
//|                                                                           |
//|    PURPOSE  : See comment above                                           |
//|                                                                           |
//|    NOTES    :                                                             |
//|---------------------------------------------------------------------------|
public class QuaternionConversion
{

    #region Attempt to Emulate Inspector Using Math
    //https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/
    // ********************************************* UNIT TEST RESULTS *********************************************
    //_checkPitchAxisOnly                                       [FAILED] -
    //_checkRollAxisOnly                                        [FAILED] -
    //_checkYawAxisOnly                                         [FAILED] -
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] -
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] -
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED] 
    //CheckRoll360_OriginState_RollOnly                         [FAILED] -
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] -
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] -
    //CheckYaw360_OriginState_RollOnly                          [FAILED] -

    /*
    public float getPitch(Transform transform)
    {
        Vector3 angle = transform.eulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }
        return x;
    }
    public float getRoll(Transform transform)
    {
        Vector3 angle = transform.eulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }
        return z;
    }

    public float getYaw(Transform transform)
    {
        Vector3 angle = transform.eulerAngles;
        float x = angle.x;
        float y = angle.y;
        float z = angle.z;

        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (angle.y > 180)
        {
            y = angle.y - 360f;
        }

        if (angle.z > 180)
        {
            z = angle.z - 360f;
        }
        return y;
    }
    */
    #endregion








    #region Needs Work - Must be close but Getting NaN Errors
    //I am getting NaN errors on pitch. I'm not sure why
    //https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/\
    //https://drive.google.com/file/d/0B9rLLz1XQKmaUHhmMExJbVZtb0U/view
    //Next thing to attempt:
    //Link to code: https://forum.unity.com/threads/rotation-order.13469/

    /*
    enum RotSeq
    {
        zyx, zyz, zxy, zxz, yxz, yxy, yzx, yzy, xyz, xyx, xzy, xzx
    };

    Vector3 twoaxisrot(float r11, float r12, float r21, float r31, float r32)
    {
        Vector3 ret = new Vector3();
        ret.x = Mathf.Atan2(r11, r12);
        ret.y = Mathf.Acos(r21);
        ret.z = Mathf.Atan2(r31, r32);
        return ret;
    }

    Vector3 threeaxisrot(float r11, float r12, float r21, float r31, float r32)
    {
        Vector3 ret = new Vector3();
        ret.x = Mathf.Atan2(r31, r32);
        ret.y = Mathf.Asin(r21);
        ret.z = Mathf.Atan2(r11, r12);
        return ret;
    }

    //Unity Default is ZXY
    //attempted xzy first got weird NaN reference on Pitch
    //attempted zyx next, got a weird NaN refernece on Roll
    //was originally pitch
    public float getRoll(Transform player)
    {
        Vector3 result = quaternion2Euler(player.transform.rotation, RotSeq.xyz);
        return result.x * (float)(180/System.Math.PI);
    }

    //was originally roll
    public float getPitch(Transform player)
    {
        Vector3 result = quaternion2Euler(player.transform.rotation, RotSeq.xyz);
        return result.z * (float)(180 / System.Math.PI);
    }
    public float getYaw(Transform player)
    {
        Vector3 result = quaternion2Euler(player.transform.rotation, RotSeq.xyz);
        return result.y * (float)(180 / System.Math.PI);
    }

    Vector3 quaternion2Euler(Quaternion q, RotSeq rotSeq)
    {
        switch (rotSeq)
        {
            case RotSeq.zyx:
                return threeaxisrot(2 * (q.x * q.y + q.w * q.z),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z,
                    -2 * (q.x * q.z - q.w * q.y),
                    2 * (q.y * q.z + q.w * q.x),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z);


            case RotSeq.zyz:
                return twoaxisrot(2 * (q.y * q.z - q.w * q.x),
                    2 * (q.x * q.z + q.w * q.y),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z,
                    2 * (q.y * q.z + q.w * q.x),
                    -2 * (q.x * q.z - q.w * q.y));


            case RotSeq.zxy:
                return threeaxisrot(-2 * (q.x * q.y - q.w * q.z),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z,
                    2 * (q.y * q.z + q.w * q.x),
                    -2 * (q.x * q.z - q.w * q.y),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z);


            case RotSeq.zxz:
                return twoaxisrot(2 * (q.x * q.z + q.w * q.y),
                    -2 * (q.y * q.z - q.w * q.x),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z,
                    2 * (q.x * q.z - q.w * q.y),
                    2 * (q.y * q.z + q.w * q.x));


            case RotSeq.yxz:
                return threeaxisrot(2 * (q.x * q.z + q.w * q.y),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z,
                    -2 * (q.y * q.z - q.w * q.x),
                    2 * (q.x * q.y + q.w * q.z),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z);

            case RotSeq.yxy:
                return twoaxisrot(2 * (q.x * q.y - q.w * q.z),
                    2 * (q.y * q.z + q.w * q.x),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z,
                    2 * (q.x * q.y + q.w * q.z),
                    -2 * (q.y * q.z - q.w * q.x));


            case RotSeq.yzx:
                return threeaxisrot(-2 * (q.x * q.z - q.w * q.y),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z,
                    2 * (q.x * q.y + q.w * q.z),
                    -2 * (q.y * q.z - q.w * q.x),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z);


            case RotSeq.yzy:
                return twoaxisrot(2 * (q.y * q.z + q.w * q.x),
                    -2 * (q.x * q.y - q.w * q.z),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z,
                    2 * (q.y * q.z - q.w * q.x),
                    2 * (q.x * q.y + q.w * q.z));


            case RotSeq.xyz:
                return threeaxisrot(-2 * (q.y * q.z - q.w * q.x),
                    q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z,
                    2 * (q.x * q.z + q.w * q.y),
                    -2 * (q.x * q.y - q.w * q.z),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z);


            case RotSeq.xyx:
                return twoaxisrot(2 * (q.x * q.y + q.w * q.z),
                    -2 * (q.x * q.z - q.w * q.y),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z,
                    2 * (q.x * q.y - q.w * q.z),
                    2 * (q.x * q.z + q.w * q.y));


            case RotSeq.xzy:
                return threeaxisrot(2 * (q.y * q.z + q.w * q.x),
                    q.w * q.w - q.x * q.x + q.y * q.y - q.z * q.z,
                    -2 * (q.x * q.y - q.w * q.z),
                    2 * (q.x * q.z + q.w * q.y),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z);


            case RotSeq.xzx:
                return twoaxisrot(2 * (q.x * q.z - q.w * q.y),
                    2 * (q.x * q.y + q.w * q.z),
                    q.w * q.w + q.x * q.x - q.y * q.y - q.z * q.z,
                    2 * (q.x * q.z + q.w * q.y),
                    -2 * (q.x * q.y - q.w * q.z));

            default:
                Debug.LogError("No good sequence");
                return Vector3.zero;

        }

    }
    */
    #endregion



    #region DOES NOT WORK 
    #region Get Euler Angles Directly
    // ********************************************* UNIT TEST RESULTS *********************************************
    //_checkPitchAxisOnly                                       [FAILED] -
    //_checkRollAxisOnly                                        [FAILED] -
    //_checkYawAxisOnly                                         [FAILED] - 
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Failed because pitch went to 0 after 180 degree rotation
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] - Failed because roll went to 0 after roll 180 (and yaw at that time went from 0 to 180)
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED] 
    //CheckRoll360_OriginState_RollOnly                         [PASSED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] - Failed because pitch goes to 0 instead of holding at 90 after simulated yaw
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] - Failed because roll goes to 0 instead of holding at 90, pitch goes to 270 instead of 0, and yaw goes to 90
    //CheckYaw360_OriginState_RollOnly                          [PASSED] 
    /*
    
    public float getPitch(Transform player)
    {
        return player.eulerAngles.x;
    }

    public float getRoll(Transform player)
    {
        return player.eulerAngles.z;
    }

    public float getYaw(Transform player)
    {
        return player.eulerAngles.y;
    }
    
    */
    #endregion

    #region Get Local Euler Angles Directly
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Failed because pitch went to 0 after 180 degree rotation
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] - Failed because roll went to 0 after roll 180 (and yaw at that time went from 0 to 180)
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED] 
    //CheckRoll360_OriginState_RollOnly                         [PASSED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] - Failed because pitch goes to 0 instead of holding at 90 after simulated yaw
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] - Failed because roll goes to 0 instead of holding at 90, pitch goes to 270 instead of 0, and yaw goes to 90
    //CheckYaw360_OriginState_RollOnly                          [PASSED] 
    /*
    public float getPitch(Transform player)
    {
        return player.localEulerAngles.x;
    }

    public float getRoll(Transform player)
    {
        return player.localEulerAngles.z;
    }

    public float getYaw(Transform player)
    {
        return player.localEulerAngles.y;
    }
    */

    #endregion

    #region Get Euler Angles from the UnityEditor (Cheating)
    //note must include using UnityEditor;
    //note this solution returns weird angles like 90.00001f instead of 90. I guess it's close enough, but it's weird
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [PASSED]
    //CheckPitch90Roll360_OriginState_AllYaw                    [PASSED] 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED] 
    //CheckRoll360_OriginState_RollOnly                         [PASSED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] - After the first 90-degree roll & pitch, reports (0,90,90) when it should be (90,90,0)
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] - After the first 90 Roll & Yaw, reports (270,90,0) when it should be (90,90,0)
    //CheckYaw360_OriginState_RollOnly                          [PASSED] 
    //link:https://answers.unity.com/questions/1589025/how-to-get-inspector-rotation-values.html
    /*
    public float getPitch(Transform player)
    {
        return UnityEditor.TransformUtils.GetInspectorRotation(player).x;
    }

    public float getRoll(Transform player)
    {
        return UnityEditor.TransformUtils.GetInspectorRotation(player).z;
    }

    public float getYaw(Transform player)
    {
        return UnityEditor.TransformUtils.GetInspectorRotation(player).y;
    }
    */
    #endregion

    #region Serialized Local Euler Hint - .NET Reflection
    //got this following attempt was based on a forum post
    //Does not seem to work because it always seems to return 0,0,0. Not sure why they said it works
    //Be sure to have the following includes for this to work:
    //using UnityEditor;
    //using System

    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Returned (0,0,0) after the first 90 degree rotation?
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED] 
    //CheckRoll360_OriginState_RollOnly                         [FAILED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] 
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] 
    //CheckYaw360_OriginState_RollOnly                          [] 


    //code from forum https://forum.unity.com/threads/how-to-get-euler-rotation-angles-exactly-as-displayed-in-the-transform-inspector-solved.425244/
    /*
    public float getPitch(Transform player)
    {
        //code from forum https://forum.unity.com/threads/how-to-get-euler-rotation-angles-exactly-as-displayed-in-the-transform-inspector-solved.425244/
        SerializedObject serializedObject = new UnityEditor.SerializedObject(player.transform);
        SerializedProperty serializedEulerHint = serializedObject.FindProperty("m_LocalEulerAnglesHint");

        return (float)Math.Round(serializedEulerHint.vector3Value.x, 2);
    }

    public float getRoll(Transform player)
    {

        SerializedObject serializedObject = new UnityEditor.SerializedObject(player.transform);
        SerializedProperty serializedEulerHint = serializedObject.FindProperty("m_LocalEulerAnglesHint");

        return (float)Math.Round(serializedEulerHint.vector3Value.z, 2);
    }

    public float getYaw(Transform player)
    {
        //code from forum https://forum.unity.com/threads/how-to-get-euler-rotation-angles-exactly-as-displayed-in-the-transform-inspector-solved.425244/
        SerializedObject serializedObject = new UnityEditor.SerializedObject(player.transform);
        SerializedProperty serializedEulerHint = serializedObject.FindProperty("m_LocalEulerAnglesHint");

        return (float)Math.Round(serializedEulerHint.vector3Value.y, 2);
    }
    */
    #endregion

    #region Erin's Quaternion-> Euler Attempt on 5/13/20
    //Erin's Quaternion->Euler attempt as of 5/15/20
    //requires a static variable for "oldRotation" and "RollAngle"
    //note that the rotation is initialized to the origin in the Start() function of her code
    //note in Erin's function getPitch(), getYaw, and getRoll must be static functions
    //I tried them as non-static functions and got these results
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Failed because pitch went to 0 after 180 degree rotation
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [FAILED] 
    //CheckRoll360_OriginState_RollOnly                         [FAILED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] 
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] 
    //CheckYaw360_OriginState_RollOnly                          []

    //I tried them as static functions and got these results
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Same as above.
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [FAILED] 
    //CheckRoll360_OriginState_RollOnly                         [FAILED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] 
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] 
    //                                                          []

    /*
    public static Quaternion OldRotation = Quaternion.identity;
    public static float RollAngle;

    public static float getPitch(Transform player)
    {
        float sine = Mathf.Clamp(player.transform.forward.y, -1.0f, 1.0f);
        float PitchAngle = -Mathf.Asin(sine) * Mathf.Rad2Deg;

        return PitchAngle;
    }

    public static float getYaw(Transform player)
    {
        float sine = Mathf.Clamp(player.transform.forward.x, -1f, 1f);
        float YawAngle = -Mathf.Asin(sine) * Mathf.Rad2Deg;

        return YawAngle;
    }

    public static float getRoll(Transform player)
    {
        var newRotation = player.transform.rotation;
        var deltaRotation = Quaternion.Inverse(OldRotation) * newRotation;

        OldRotation = newRotation;

        RollAngle = (RollAngle - deltaRotation.eulerAngles.z) % 360.0f;

        /*
		// Calculate roll & pitch angles
		// Calculate the flat forward direction (with no y component).
		var flatForward = player.forward;
		flatForward.y = 0;
		// If the flat forward vector is non-zero (which would only happen if the plane was pointing exactly straight upwards)
		if (flatForward.sqrMagnitude > 0)
		{
			flatForward.Normalize();

			// calculate current roll angle
			var flatRight = Vector3.Cross(Vector3.up, flatForward);
			var localFlatRight = player.InverseTransformDirection(flatRight);
			return Mathf.Atan2(localFlatRight.y, localFlatRight.x) * Mathf.Rad2Deg;
		}//end code here

        return RollAngle;
    }
    */
    #endregion

    #region Quaternion Unwinging - Its Close - But Seems to be in the Wrong Order
    //this code seems close but unfortunately it doesn't seem to produce the same result that we were expecting
    //I think the problem is that it is prioritizing rotations in the form yaw, roll, then pitch.
    //code originally from this link: https://forum.unity.com/threads/roll-pitch-and-yaw-from-quaternion.63498/
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - Pitch reports 0 after first 90 degree pitch rotation (0,90,0) reported vs (90,0,0) expected 
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] - Pitch returned 270 after roll passed 90 degrees (270,90,0) reported vs (90,90,0) expected
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED]
    //CheckRoll360_OriginState_RollOnly                         [FAILED] - Yaw reported 90 when roll should have only moved. (0,0,90) reported vs (0,90,0) expected.
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] - After the first 90-degree roll & pitch, reports (0,90,90) when it should be (90,90,0)
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] - After the first 90 Roll & Yaw, reports (270,90,0) when it should be (90,90,0)
    //CheckYaw360_OriginState_RollOnly                          [FAILED] - Pitch moved after 90 degrees Yaw. Only Yaw should have moved. (90,0,0) reported vs (0,0,90) expected


    //originally these functions (see link above) said getYaw, getRoll, and getPitch, but the values seemed incorrect
    //so I changed them to now say getRoll, getPitch, and getYaw, although this makes two additional unit tests pass 
    //it's not enough to get all the unit tests to pass
    /*
    public float getRoll(Transform t)
    {
        return t.localEulerAngles.z;
    }

    public float getPitch(Transform originalTransform)
    {
        GameObject tempGO = new GameObject();
        Transform t = tempGO.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);

        //GameObject.Destroy(tempGO);
        return t.localEulerAngles.x;
    }

    public float getYaw(Transform originalTransform)
    {
        GameObject tempGO = new GameObject();
        Transform t = tempGO.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);
        t.Rotate(t.localEulerAngles.x * -1, 0, 0);

        //GameObject.Destroy(tempGO);
        return t.localEulerAngles.y;
    }
    */
    #endregion

    #region Quaternion -> Euler using Math Equations Attempt #3
    //Link to post https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html
    //NOTE: there are multiple solutions in the post. This one is the final answer from  "ratneshpatel · Feb 14 at 08:07 AM"  
    //NOTE: this solution returns values from -180 to +180
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [PASSED]
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] - Returned 0 for Roll when 90 was expected. Roll was converted to Yaw. 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED]
    //CheckRoll360_OriginState_RollOnly                         [FAILED] - Returned 180 for Pitch when 0 was expected
    //CheckRoll90Pitch360_OriginState_AllYaw                    [FAILED] - Returned 0 for pitch when 90 was expected
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] - Returned 270 for pitch when 90 was expected
    //CheckYaw360_OriginState_RollOnly                          []
    /*
    public float getPitch(Transform transform)
    {
        Quaternion q = transform.rotation;
        float Pitch = Mathf.Rad2Deg * Mathf.Atan2(2 * q.x * q.w - 2 * q.y * q.z, 1 - 2 * q.x * q.x - 2 * q.z * q.z);
        Pitch = (float)Math.Round(Pitch, 2);

        return Pitch;
    }

    public float getRoll(Transform transform)
    {
        Quaternion q = transform.rotation;
        float Roll = Mathf.Rad2Deg * Mathf.Asin(2 * q.x * q.y + 2 * q.z * q.w);
        Roll = (float)Math.Round(Roll, 2);

        return Roll;
    }

    public float getYaw(Transform transform)
    {
        Quaternion q = transform.rotation;
        float Yaw = Mathf.Rad2Deg * Mathf.Atan2(2 * q.y * q.w - 2 * q.x * q.z, 1 - 2 * q.y * q.y - 2 * q.z * q.z);
        Yaw = (float)Math.Round(Yaw);

        return Yaw;
    }
    */
    #endregion

    #region Using Quaternion.LookAt() to try to derive the angle relative to the origin
    //inspired be the code at link: https://www.youtube.com/watch?time_continue=4&v=6zeqbQkunsA&feature=emb_logo
    //however this doesn't work because the LookAt() is not the core problem
    //if we are located above the origin and pitch down, it will still generate a vector that looks up
    /*
    public float getPitch(Transform player)
    {
        //this should be at the origin because it was just created
        //hopefully that's a safe assumption
        GameObject origin = new GameObject();

        Vector3 direction = (player.position - origin.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        return lookRotation.eulerAngles.x;
    }

    public float getRoll(Transform player)
    {
        //this should be at the origin because it was just created
        //hopefully that's a safe assumption
        Rigidbody origin = new Rigidbody();

        Vector3 direction = (player.position - origin.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        return lookRotation.eulerAngles.z;
    }
    public float getYaw(Transform player)
    {
        //this should be at the origin because it was just created
        //hopefully that's a safe assumption
        Rigidbody origin = new Rigidbody();

        Vector3 direction = (player.position - origin.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        return lookRotation.eulerAngles.y;
    }
    */
    #endregion

    #region Aviation Inspired Quaternion Conversion into Attitude, Bank, And Heading - Doesnt work - must be problem with left-hand rule
    //code inspired by link: http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/index.htm
    //Jack Morrison appears to be the brains behind this code:
    //http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/jack.htm
    /*
    public float getPitch(Transform player)
    {
        double attitude = 0.0, heading = 0.0f, bank = 0.0f;
        quaternionHelper(player.rotation, out heading, out attitude, out bank);
        return (float)bank;
    }

    public float getRoll(Transform player)
    {
        double attitude = 0.0, heading = 0.0f, bank = 0.0f;
        quaternionHelper(player.rotation, out heading, out attitude, out bank);
        return (float)attitude;
    }
    public float getYaw(Transform player)
    {
        double attitude = 0.0, heading = 0.0f, bank = 0.0f;
        quaternionHelper(player.rotation, out heading, out attitude, out bank);
        return (float)heading;
    }

    private void quaternionHelper(Quaternion q1, out double heading, out double attitude, out double bank)
    {
        double test = q1.x * q1.y + q1.z * q1.w;
        if (test > 0.499)
        { // singularity at north pole
            heading = 2 * System.Math.Atan2(q1.x, q1.w) * (180 / System.Math.PI);
            attitude = System.Math.PI / 2 * (180 / System.Math.PI);
            bank = 0;
            return;
        }
        if (test < -0.499)
        { // singularity at south pole
            heading = -2 * System.Math.Atan2(q1.x, q1.w) * (180 / System.Math.PI);
            attitude = -System.Math.PI / 2 * (180/System.Math.PI);
            bank = 0;
            return;
        }
        double sqx = q1.x * q1.x;
        double sqy = q1.y * q1.y;
        double sqz = q1.z * q1.z;

        heading = System.Math.Atan2(2 * q1.y * q1.w - 2 * q1.x * q1.z, 1 - 2 * sqy - 2 * sqz) * (180 / System.Math.PI);
        attitude = System.Math.Asin(2 * test) * (180 / System.Math.PI);
        bank = System.Math.Atan2(2 * q1.x * q1.w - 2 * q1.y * q1.z, 1 - 2 * sqx - 2 * sqz) * (180 / System.Math.PI);
    }
    */
    #endregion

    #region Alex's Silly Attempt to Use Transform.up, Transform.Right, and Transform.Forward

    //Alex's Silly Attempt
    //No way it would be this easy, we would have figured it out long ago
    //Seems to return values from -1 to +1, which doesn't make any sense to me
    
    public float getPitch(Transform player)
    {
        return player.up.x;
    }

    public float getRoll(Transform player)
    {
        return player.forward.z;
    }

    public float getYaw(Transform player)
    {
        return player.right.y;
    }
    

    #endregion
    #region Alex's Silly Attempt to Use Transform.up, Transform.Forward, and Transform.Right - Attempt #2
    //another silly attempt using Quaternion.FromToRotation
    //link: https://docs.unity3d.com/ScriptReference/Quaternion.FromToRotation.html
    /*
    public float getPitch(Transform player)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, player.forward);
        return rotation.eulerAngles.x;
    }

    //another silly attempt using Quaternion.FromToRotation
    //link: https://docs.unity3d.com/ScriptReference/Quaternion.FromToRotation.html
    public float getYaw(Transform player)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.right, player.forward);
        return rotation.eulerAngles.y;
    }

    //another silly attempt using Quaternion.FromToRotation
    //link: https://docs.unity3d.com/ScriptReference/Quaternion.FromToRotation.html
    public float getRoll(Transform player)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, player.forward);
        return rotation.eulerAngles.z;
    }
    */
    #endregion
    #region Alex's Silly Attempt to Use Vector.Angle to calculate an angle between the world origin and the player transform
    //another silly attempt using Vector3.Angle
    /*
    public float getPitch(Transform player)
    {
        return Vector3.Angle(Vector3.forward, player.forward);
    }
    public float getRoll(Transform player)
    {
        return Vector3.Angle(Vector3.forward, player.up);
    }

    public float getYaw(Transform player)
    {
        return Vector3.Angle(Vector3.right, player.right);
    }
    */
    #endregion

    #region Using Vector.Cross and trying to calculate Angles
    //Link: https://answers.unity.com/questions/1366142/get-pitch-and-roll-values-from-object.html
    //I tried the 2nd solution proposed by "Bunny83"
    // ********************************************* UNIT TEST RESULTS *********************************************
    //CheckPitch360_OriginState_PitchOnly                       [FAILED] - comes up with angle of -90 for pitch when +90 was expected
    //CheckPitch90Roll360_OriginState_AllYaw                    [FAILED] 
    //CheckPitchRollYawWithNoMovement_OriginState_NoMovement    [PASSED]
    //CheckRoll360_OriginState_RollOnly                         [FAILED] 
    //CheckRoll90Pitch360_OriginState_AllYaw                    [PASSED] 
    //CheckRoll90Yaw360_OriginState_AllPitch                    [FAILED] 
    //CheckYaw360_OriginState_RollOnly                          [PASSED] 
    /*
    public float getPitch(Transform player)
    {
        var right = player.right;
        right.y = 0;
        right *= Mathf.Sign(player.up.y);
        var fwd = Vector3.Cross(right, Vector3.up).normalized;
        return Vector3.Angle(fwd, player.forward) * Mathf.Sign(player.forward.y);
    }

    public float getYaw(Transform player)
    {
        return player.eulerAngles.y;
    }

    public float getRoll(Transform player)
    {
        var fwd = player.forward;
        fwd.y = 0;
        fwd *= Mathf.Sign(player.up.y);
        var right = Vector3.Cross(Vector3.up, fwd).normalized;
        return Vector3.Angle(right, player.right) * Mathf.Sign(player.right.y);
    }
    */
    #endregion
    #endregion
}