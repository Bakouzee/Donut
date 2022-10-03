using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public interface IFollowable {

    Character target { get; set; }
    float range { get; set; }
    Direction direction { get; set; }
    
    bool isFollowing { get; set; }
    bool lastFollow { get; set; }

    public void OnFollowBegin();
    public void Follow();
    public void OnFollowEnd();
    
    

}
