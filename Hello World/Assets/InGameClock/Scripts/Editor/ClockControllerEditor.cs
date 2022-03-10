using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClockController))]
public class ClockControllerEditor : Editor
{
    public SerializedProperty
        clockType,
        use12HourFormat,
        timeSpeed,
        timeScale,
        hoursForDay,
        useSystemTime,
        hours,
        minutes,
        seconds,
        showSeconds;
    


    void OnEnable () {
        clockType = serializedObject.FindProperty ("clockType");
        use12HourFormat = serializedObject.FindProperty ("use12HourFormat");
        timeSpeed = serializedObject.FindProperty ("timeSpeed");
        timeScale = serializedObject.FindProperty ("timeScale");
        hoursForDay = serializedObject.FindProperty ("hoursForDay");
        useSystemTime = serializedObject.FindProperty("useSystemTime");
        hours = serializedObject.FindProperty("hours");
        minutes = serializedObject.FindProperty("minutes");
        seconds = serializedObject.FindProperty("seconds");
        showSeconds = serializedObject.FindProperty("showSeconds");
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        serializedObject.Update ();
         
        EditorGUILayout.PropertyField( this.clockType );
        
        ClockController.ClockType clockType =(ClockController.ClockType)this.clockType.enumValueIndex;
         
        switch( clockType ) {
            case ClockController.ClockType.Digital:            
                EditorGUILayout.PropertyField( use12HourFormat, new GUIContent("Use 12 Hour Format") );
                break;
             
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(useSystemTime);

        if (!useSystemTime.boolValue)
        {
            EditorGUILayout.IntSlider(hours, 0, 23, new GUIContent("Hours"));
            EditorGUILayout.IntSlider(minutes, 0, 59, new GUIContent("Minutes"));
            EditorGUILayout.IntSlider(seconds, 0, 59, new GUIContent("Seconds"));
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(showSeconds, new GUIContent("Display Seconds"));

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(this.timeSpeed);

        ClockController.TimeSpeed timeSpeed = (ClockController.TimeSpeed) this.timeSpeed.enumValueIndex;

        switch (timeSpeed)
        {
            case ClockController.TimeSpeed.TimeScale:
                EditorGUILayout.PropertyField(timeScale, new GUIContent("Time Scale", "1 = Realtime, 2 = 2x Faster, etc"));
                break;
            
            case ClockController.TimeSpeed.HourForDay:
                EditorGUILayout.PropertyField(hoursForDay, new GUIContent("Hour For Day", "Real life hours for 1 in game day, 24 hours = realtime, 12 hour = 2x faster, 1 hour = 24x faster"));
                break;
        }
         
        serializedObject.ApplyModifiedProperties ();
    }
}