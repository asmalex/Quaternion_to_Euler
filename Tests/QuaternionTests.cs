//|---------------------------------------------------------------------------|
//|    FILE NAME: QuaternionTests.cs                                          |
//|                                                                           |
//|    AUTHOR   : Alexander Redei                                             |
//|                                                                           |
//|    PURPOSE  : Tests Quaternion to Euler conversions                       |
//|                                                                           |
//|               Most important is the Pitch and Roll telemetry as those are |
//|                 what's primarily used by the flight simualtor. The        |
//|                 additional telemetry such as Surge may be used in the     |
//|					future to create a more complete motion profile.		  |
//|                                                                           |
//|               The following items are tested:                             |
//|                 Basic Ground Truth Tests:                                 |
//|                 1) Rotating Nothing -> Returns 0,0,0                      |
//|                 2) Rotating Roll 360 -> Only roll is affected             |
//|                 3) Rotating Pitch 360 -> Only pitch is affected           |
//|                                                                           |
//|    NOTES    : Unity x axis corresponds with Pitch, y axis corresponds     |
//|                with Yaw, and z axis corresponds with Roll.                |
//|                                                                           |
//|               All unit tests must start with "Check"... it's important    |
//|                that the "C" be capitalized in order for the following     |
//|                tests to work:                                             |
//|                         _checkPitchAxisOnly()                             |
//|                         _checkRollAxisOnly()                              |
//|                         _checkYawAxisOnly()                               |
//|                                                                           |
//|                                                                           |
//|    REVISIONS:                                                             |
//|			   5/15/20 - Alex R. - Created w/ 4 Unit Tests for Basic Truths   |
//|			   5/15/20 - Alex R. - Added tests for 360 pitch, roll, & yaw     |
//|			   5/16/20 - Alex R. - Added tests for sequential move testing    |
//|			   5/16/20 - Alex R. - Added tests for independent axis testing   |
//|---------------------------------------------------------------------------|

using NUnit.Framework;
using UnityEngine;
using System.Reflection;

namespace Tests
{
    //|---------------------------------------------------------------------------|
    //|    CLASS    : Quaternion_Tests                                            |
    //|                                                                           |
    //|    PURPOSE  : See comment above. Unit tests for Quaternion conversion     |
    //|                                                                           |
    //|    NOTES    : Tests are named as "TestCondition_InitState_ExpectedResult" |
    //|---------------------------------------------------------------------------|
    public class Quaternion_Tests
    {
        #region Member Variables
        public static QuaternionConversion _converter = new QuaternionConversion();
        private GameObject _player;
        private bool _checkPitch = true;
        private bool _checkRoll = true;
        private bool _checkYaw = true;
        #endregion

        #region Ground Truth Tests
        //checks the origin state
        [Test]
        public void CheckPitchRollYawWithNoMovement_OriginState_NoMovement()
        {
            //arrange
            _player = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            //act:      do nothing
            _player.transform.Rotate(0f, 0f, 0f);
            //assert:   check Euler
            checkRotations(0, 0, 0);

            //old code: this was moved to the checkRotations() helper function
            //Assert.AreEqual(0.0f, _converter.getPitch(_player.transform));
            //Assert.AreEqual(0.0f, _converter.getYaw(_player.transform));
            //Assert.AreEqual(0.0f, _converter.getRoll(_player.transform));
            //sanity check the assert
            //Assert.AreEqual(-0f, 0f);
        }

        // Checks Positive Roll 360
        [Test]
        public void CheckPitch360_OriginState_PitchOnly()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(90, 0, 0);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(180, 0, 0);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(270, 0, 0);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(0, 0, 0);
        }

        // Checks Positive Roll 360
        [Test]
        public void CheckRoll360_OriginState_RollOnly()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(0, 0, 90);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(0, 0, 180);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
           checkRotations(0, 0, 270);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(0, 0, 0);
        }

        // Checks Positive Yaw 360
        [Test]
        public void CheckYaw360_OriginState_YawOnly()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler
            checkRotations(0, 90, 0);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler
            checkRotations(0, 180, 0);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler
            checkRotations(0, 270, 0);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler
            checkRotations(0, 0, 0);
        }

        #endregion

        #region Pitch 90 -> Roll Movements - Simulator Movement Tests
        // Checks if you roll around when fully pitched up in the simulator.
        [Test]
        public void CheckPitch90Roll360_OriginState_AllYaw()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      pitch +90 degrees up and roll 90 degrees
            pitch90();
            roll90();
            //assert:   check Euler
            checkRotations(90, 0, 90);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(90, 0, 180);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(90, 0, 270);

            //act:      roll +90 degrees
            roll90();
            //assert:   check Euler
            checkRotations(90, 0, 0);
        }
        #endregion

        #region Roll 90 -> Pitch Movements - Simulated Yaw Movement - No Simulator Movement Tests
        // Checks Simulated Yaw doesn't produce simulator movement
        [Test]
        public void CheckRoll90Pitch360_OriginState_AllYaw()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      roll +90 degrees and pitch up 90 degrees
            roll90();
            pitch90();
            //assert:   check Euler
            checkRotations(0, 90, 90);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(0, 180, 90);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(0, 270, 90);

            //act:      pitch +90 degrees
            pitch90();
            //assert:   check Euler
            checkRotations(0, 0, 90);
        }
        #endregion

        #region Roll 90 -> Yaw Movements - Simulated Pitch Movement - Simulator Movement Tests
        // Checks Simulated Pitch w/ a Yaw Gimbal Lock produces simulator movement
        [Test]
        public void CheckRoll90Yaw360_OriginState_AllPitch()
        {
            //arrange
            _player = new GameObject("Player");

            //act:      roll +90 degrees and Yaw up 90 degrees
            roll90();
            yaw90();
            //assert:   check Euler. Note that Pitch should be moving instead of Yaw
            checkRotations(90, 0, 90);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler. Note that Pitch should be moving instead of Yaw
            checkRotations(180, 0, 90);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler. Note that Pitch should be moving instead of Yaw
            checkRotations(270, 0, 90);

            //act:      yaw +90 degrees
            yaw90();
            //assert:   check Euler. Note that Pitch should be moving instead of Yaw
            checkRotations(0, 0, 90);

            //act:      roll back to zero +270 degrees
            roll90();
            roll90();
            roll90();
            //assert:   check Euler
            checkRotations(0, 0, 0);
        }
        #endregion

        #region Check Pitch Axis Only - Repeat All Unit Tests Above
        /// <summary>
        /// Repeats all the unit tests, but only checking if the pitch axis is correct
        /// </summary>
        /// <remarks>Needs System.reflection to work</remarks>
        [Test]
        public void _checkPitchAxisOnly()
        {
            //I know this probably seems confusing but...
            //Since we are using recursion, we need to make sure we *DO NOT* repeat this test if we are
            //within a sub=obejct of the parent unit test. Therefore, if all the flags are not set to true
            // then it must mean that we are inside a created sub-object unit test and 
            // there is no need to re-run th tests again
            if (_checkPitch && _checkRoll && _checkPitch) // this means we are in the parent unit test
            {
                //arrange
                Quaternion_Tests myTestObject = new Quaternion_Tests();
                myTestObject._checkPitch = true;
                myTestObject._checkRoll = false;
                myTestObject._checkYaw = false;

                reflectAllUnitTests(myTestObject);
            }
        }
        #endregion

        #region Check Roll Axis Only - Repeat All Unit Tests Above
        /// <summary>
        /// Repeats all the unit tests, but only checking if the roll axis is correct
        /// </summary>
        /// <remarks>Needs System.reflection to work</remarks>
        [Test]
        public void _checkRollAxisOnly()
        {
            //I know this probably seems confusing but...
            //Since we are using recursion, we need to make sure we *DO NOT* repeat this test if we are
            //within a sub=obejct of the parent unit test. Therefore, if all the flags are not set to true
            // then it must mean that we are inside a created sub-object unit test and 
            // there is no need to re-run th tests again
            if (_checkPitch && _checkRoll && _checkPitch) // this means we are in the parent unit test
            {
                //arrange
                Quaternion_Tests myTestObject = new Quaternion_Tests();
                myTestObject._checkPitch = false;
                myTestObject._checkRoll = true;
                myTestObject._checkYaw = false;

                reflectAllUnitTests(myTestObject);
            }
        }
        #endregion

        #region Check Yaw Axis Only - Repeat All Unit Tests Above
        /// <summary>
        /// Repeats all the unit tests, but only checking if the yaw axis is correct
        /// </summary>
        /// <remarks>Needs System.reflection to work</remarks>
        [Test]
        public void _checkYawAxisOnly()
        {
            //I know this probably seems confusing but...
            //Since we are using recursion, we need to make sure we *DO NOT* repeat this test if we are
            //within a sub=obejct of the parent unit test. Therefore, if all the flags are not set to true
            // then it must mean that we are inside a created sub-object unit test and 
            // there is no need to re-run th tests again
            if (_checkPitch && _checkRoll && _checkPitch) // this means we are in the parent unit test
            {
                //arrange
                Quaternion_Tests myTestObject = new Quaternion_Tests();
                myTestObject._checkPitch = false;
                myTestObject._checkRoll = false;
                myTestObject._checkYaw = true;

                reflectAllUnitTests(myTestObject);
            }
        }
        #endregion






        #region Helper Functions
        /// <summary>
        /// Checks the outputted rotation matrix to the desired rotation matrix. 
        /// </summary>
        /// <param name="pitch">The pitch value from 0-360 in degrees</param>
        /// <param name="yaw">The yaw value from 0-360 in degrees</param>
        /// <param name="roll">The roll value from 0-360 in degrees</param>
        /// <remarks>Need to find a way to support radians as well</remarks>
        private void checkRotations(float pitch, float yaw, float roll)
        {
            //get pitch, roll, and yaw from the converter
            float p = _converter.getPitch(_player.transform);
            float r = _converter.getRoll(_player.transform);
            float y = _converter.getYaw(_player.transform);

            //if the rotations returned are negative, put them on a 0 to +360 scale to compare
            p = (p < 0) ? (p + 360) : p;
            r = (r < 0) ? (r + 360) : r;
            y = (y < 0) ? (y + 360) : y;

            //log the output so we don't have to walk through it with a breakpoint each time
            Debug.Log(@"The calculated rotations were: "
                        + "p: " + p + " r: " + r + " y: " + y);
            Debug.Log(@" compared to known values: "
                        + "p: " + pitch + " r: " + roll + " y: " + yaw);

            //NOTE: the third parameter in the assert checks that the values are within 1/2 a degree
            //floating point errors can do some weird things, so if it's within a half a degree
            //it's good enough
            if(_checkPitch)
                Assert.AreEqual(pitch, p, 0.5f);
            if(_checkRoll)
                Assert.AreEqual(roll, r, 0.5f);
            if(_checkYaw)
                Assert.AreEqual(yaw, y, 0.5f);
        }

        private void pitch90()
        {
            Debug.Log("Pitching 90-degrees");
            _player.transform.Rotate(90f, 0f, 0f);
        }
        private void roll90()
        {
            Debug.Log("Rolling 90-degrees");
            _player.transform.Rotate(0f, 0f, 90f);
        }

        private void yaw90()
        {
            Debug.Log("Yawing 90-degrees");
            _player.transform.Rotate(0f, 90f, 0f);
        }

        private void pitch45()
        {
            Debug.Log("Pitching 45-degrees");
            _player.transform.Rotate(45f, 0f, 0f);
        }
        private void roll45()
        {
            Debug.Log("Rolling 45-degrees");
            _player.transform.Rotate(0f, 0f, 45f);
        }

        private void yaw45()
        {
            Debug.Log("Yawing 45-degrees");
            _player.transform.Rotate(0f, 45f, 0f);
        }

        private void reflectAllUnitTests(Quaternion_Tests myTestObject)
        {
            //retrieve all the test method names in this test class using .NET reflection 
            //note that this is only getting the public members
            MethodInfo[] methodInfos = typeof(Quaternion_Tests).GetMethods();

            //call all the tests in this test class again
            foreach (MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.Name.Contains("Check"))
                {
                    Debug.Log("------Running Test [" + methodInfo.Name + "]------");
                    methodInfo.Invoke(myTestObject, null);
                }
            }
        }

        #endregion

    }
}
