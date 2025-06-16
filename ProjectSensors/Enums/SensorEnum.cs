public enum SensorType
{
    Audio,      // בסיסי, אין יכולת מיוחדת
    Thermal,    // מגלה 
    Pulse,      // נשבר אחרי 3 הפעלות
    Motion,     // יכול לפעול 3 פעמים ואז נשבר
    Magnetic,   // מבטל התקפת נגד פעמיים אם תואם
    Signal,     // מגלה שדה מידע אחד
    Light       // מגלה שני שדות מידע
}
