using UnityEngine;
using UnityEngine.UI;

using System;
using System.Timers;

public class countdown : MonoBehaviour {

    public Text clockText;
    public int interval;

    private Timer timer;
    private DateTime elapsed;
    private DateTime empty;

	// Use this for initialization
	void Start () {

        int interval_ms = interval*1000;

        empty = new DateTime();

        TimeSpan period = TimeSpan.FromMilliseconds(interval_ms);

	    timer = new Timer(interval_ms);	

        timer.AutoReset = false;
        timer.Elapsed += OnTimedEvent;

        elapsed = DateTime.Now.Add(period);
        timer.Start();
	}
	
	// Update is called once per frame
	void Update () {
        DateTime now = DateTime.Now;

        if (timer.Enabled && elapsed.CompareTo(now) > 0) {
            clockText.text = (empty + elapsed.Subtract(now)).ToString("mm:ss");
        }
	}

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        Debug.Log("exploded");
    }
}
