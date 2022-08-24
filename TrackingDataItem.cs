/*
 * microp11 2022, Paul Maxan
 * 
 * This file is part of the telescope below.
 * 
 * The telescope is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * The code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with the telescope.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

/*
 * Format of string:
 * SNxxx AZxxx.x ELxx.x DNxxxx UPxxxx DMxxxx UMxxxx
 * Fields:
 * SN  - tracked satellite name (spaces converted to "_")
 * AZ  - azimuth in range form 0.0 to 360.0
 * EL  - elevation in range from -90.0 to 90.0
 *       please note possibility of negative values
 * DN  - downlink frequency with doppler, [Hz] in range from 0 to N
 * UP  - uplink frequency with doppler, [Hz] in range from 0 to N
 * DM  - downlink mode (optional field)
 * UM  - uplink mode (optional field)
 * Examples:
 * SNISS_ZARYA AZ182.1 EL36.3 DN145800000 UP145800000 UMFM-N
 * SNISS_ZARYA AZ180.3 EL-10.1 DN145800000 UP145800000
 * 
 * Added "newAZ" to allow for manual spillover as of to not have the rotor going 360 when passing over zero.
 * Within 10 degrees on both sides when manually selected.
 */

namespace telescope
{
    public static class Locker
    {
        public static object locker = new();
    }

    public class TrackingDataItem
    {
        internal string SN = "";
        internal float AZ = 0.0F;
        internal float EL = 0.0F;
        internal int DN = 0;
        internal int UP = 0;
        internal string DM = "";
        internal string UM = "";
        internal float newAZ = 0.0F;
    }
}
