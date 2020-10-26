using UnityEngine;

namespace Dacen.Utility
{
    public static class DacenUtility
    {
        public static string ConvertToTwoDigits(int number)
        {
            return number < 10 ? "0" + number : number.ToString();
        }

        public static string ConvertToMinutesAndSeconds(int time)
        {
            return ConvertToTwoDigits(Mathf.FloorToInt(time / 60)) + ":" + ConvertToTwoDigits(time % 60);
        }
    }
}
