using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string Name;
    public string CardImage;
    public string Background;
    public string Road;
    public int InitScore;

    public List<Point> Path;
    public List<Point> Holder;
    public List<Round> Rounds;

    public Level()
    {
        Path = new List<Point>();
        Holder = new List<Point>();
        Rounds = new List<Round>();
    }
}
