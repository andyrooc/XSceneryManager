using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSceneryManager
{
    static class Geo
    {

        static public double CalculateGreatCircleDistance(double lat1, double long1, double lat2, double long2, double radius)
        {
            return radius * Math.Acos(
                Math.Sin(lat1) * Math.Sin(lat2)
                + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1));
        }

        const double _eQuatorialEarthRadius = 6378.1370D;
        const double _d2r = (Math.PI / 180D);

        static public int HaversineInM(double lat1, double long1, double lat2, double long2)
        {
            return (int)(1000D * HaversineInKM(lat1, long1, lat2, long2));
        }

        static public double HaversineInKM(double lat1, double long1, double lat2, double long2)
        {
            double dlong = (long2 - long1) * _d2r;
            double dlat = (lat2 - lat1) * _d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _eQuatorialEarthRadius * c;

            return d;
        }


        static private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        static private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        static public double GetDirection(double lat1, double lon1, double lat2, double lon2) //rumb line
        {
            Position p1 = new Position(lat1, lon1);
            Position p2 = new Position(lat2, lon2);

            return Math.Round(CalculateRhumbBearing(p1, p2));
        }

        static public double CalculateRhumbBearing(Position position1, Position position2)
        {
            var lat1 = AngleConverter.ConvertDegreesToRadians(position1.Latitude);
            var lat2 = AngleConverter.ConvertDegreesToRadians(position2.Latitude);
            var dLon = AngleConverter.ConvertDegreesToRadians(position2.Longitude - position1.Longitude);

            var dPhi = Math.Log(Math.Tan(lat2 / 2 + Math.PI / 4) / Math.Tan(lat1 / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI) dLon = (dLon > 0) ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            var brng = Math.Atan2(dLon, dPhi);

            return (AngleConverter.ConvertRadiansToDegrees(brng) + 360) % 360;
        }

        static public double GetRumbLineBearing(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = deg2rad(lat2 - lat1);
            double dLon = deg2rad(lon2 - lon1);

            double dPhi = Math.Log(Math.Tan(Math.PI / 4 + lat2 / 2) / Math.Tan(Math.PI / 4 + lat1 / 2));
            double q = (!Double.IsInfinity(dLat / dPhi)) ? dLat / dPhi : Math.Cos(lat1);  // E-W line gives dPhi=0

            // if dLon over 180° take shorter rhumb across anti-meridian:
            if (Math.Abs(dLon) > Math.PI)
            {
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            }

            double d = Math.Sqrt(dLat * dLat + q * q * dLon * dLon) * _eQuatorialEarthRadius;
            double brng = Math.Atan2(dLon, dPhi);

            return brng;
        }
    }

    public static class AngleConverter
    {
        public static double ConvertDegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double ConvertRadiansToDegrees(double angle)
        {
            return 180.0 * angle / Math.PI;
        }
    }

    public class Position
    {
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
/*
 	

var dPhi = Math.log(Math.tan(Math.PI/4+lat2/2)/Math.tan(Math.PI/4+lat1/2));
var q = (isFinite(dLat/dPhi)) ? dLat/dPhi : Math.cos(lat1);  // E-W line gives dPhi=0

// if dLon over 180° take shorter rhumb across anti-meridian:
if (Math.abs(dLon) > Math.PI) {
  dLon = dLon>0 ? -(2*Math.PI-dLon) : (2*Math.PI+dLon);
}

var d = Math.sqrt(dLat*dLat + q*q*dLon*dLon) * R;
var brng = Math.atan2(dLon, dPhi);
*/