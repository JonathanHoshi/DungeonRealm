using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseSOBase : EnemySOBase
{
    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() 
    { 
        // Do change state
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyController.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
