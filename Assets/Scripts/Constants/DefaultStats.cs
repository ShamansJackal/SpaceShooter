using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DefaultStats
{
    public static Rigidbody2D rigidbody2D;
    public static CollisionDetectionMode2D collisionDetectionMode = CollisionDetectionMode2D.Continuous;

    public const int DefaultSpeed = 300;
    public const int DefaultDamage = 10;
    public const int DefaultCooldown = 500;

    public static Dictionary<DamageType, float> ShieldScale = new Dictionary<DamageType, float> {
        { DamageType.Balistic, 1.2f  },
        { DamageType.Laser,    1.0f  },
        { DamageType.Rocket,   1.1f  }
    };

    public static Dictionary<DamageType, float> ArmorScale = new Dictionary<DamageType, float> {
        { DamageType.Balistic, 1.0f  },
        { DamageType.Laser,    1.2f  },
        { DamageType.Rocket,   1.1f  }
    };
}


