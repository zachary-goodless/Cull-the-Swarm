using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    public GameObject bulletPrefab;
    public int poolSize = 1000;
    [HideInInspector]
    public static Sprite[] spriteList;
    [HideInInspector]
    public static Material[] bulletMats;
    public static Dictionary<BulletType, BulletTypeProperties> propertyList;
    public static PoolSystem bulletPool;
    public static GameObject shotID;
    public static BulletManager manager;
    private static float disableTime;
    private static bool duplicateExists = false;
    private static GameObject duplicateObj;

    void Update()
    {
        if (GameObject.FindWithTag("Duplicate") != null)
        {
            duplicateObj = GameObject.FindWithTag("Duplicate");
            duplicateExists = true;
        }
        else duplicateExists = false;
    }

    // Use this for initialization
    void Start ()
    {
        manager = this;
        gameObject.AddComponent<PoolSystem>();
        bulletPool = gameObject.GetComponent<PoolSystem>();
        bulletPool.Initialize(poolSize, bulletPrefab);
        spriteList = Resources.LoadAll<Sprite>("EnemyBullets/Shots");
        bulletMats = new Material[] { Resources.Load<Material>("EnemyBullets/DefaultBullet"), Resources.Load<Material>("EnemyBullets/AdditiveBlendBullet") };


        // Suffering (Determines bullet sprite number, hitbox, additive blend state, animation, then associates those properties with the enum)
        propertyList = new Dictionary<BulletType, BulletTypeProperties>();
        propertyList.Add(BulletType.RedDot, new BulletTypeProperties(95, 4, false, null));
        propertyList.Add(BulletType.RedDarkDot, new BulletTypeProperties(92, 4, false, null));
        propertyList.Add(BulletType.RedShard, new BulletTypeProperties(101, 4, false, null));
        propertyList.Add(BulletType.RedArrow, new BulletTypeProperties(84, 4, false, null));
        propertyList.Add(BulletType.RedDarkArrow, new BulletTypeProperties(89, 4, false, null));
        propertyList.Add(BulletType.RedCrawler, new BulletTypeProperties(87, 4, false, spriteListSection(87, 2)));
        propertyList.Add(BulletType.RedFire, new BulletTypeProperties(96, 4, true, spriteListSection(96, 4)));
        propertyList.Add(BulletType.YellowDot, new BulletTypeProperties(129, 4, false, null));
        propertyList.Add(BulletType.YellowDarkDot, new BulletTypeProperties(126, 4, false, null));
        propertyList.Add(BulletType.YellowShard, new BulletTypeProperties(135, 4, false, null));
        propertyList.Add(BulletType.YellowArrow, new BulletTypeProperties(118, 4, false, null));
        propertyList.Add(BulletType.YellowDarkArrow, new BulletTypeProperties(123, 4, false, null));
        propertyList.Add(BulletType.YellowCrawler, new BulletTypeProperties(121, 4, false, spriteListSection(121, 2)));
        propertyList.Add(BulletType.YellowFire, new BulletTypeProperties(130, 4, true, spriteListSection(130, 4)));
        propertyList.Add(BulletType.GreenDot, new BulletTypeProperties(45, 4, false, null));
        propertyList.Add(BulletType.GreenDarkDot, new BulletTypeProperties(42, 4, false, null));
        propertyList.Add(BulletType.GreenShard, new BulletTypeProperties(47, 4, false, null));
        propertyList.Add(BulletType.GreenArrow, new BulletTypeProperties(34, 4, false, null));
        propertyList.Add(BulletType.GreenDarkArrow, new BulletTypeProperties(39, 4, false, null));
        propertyList.Add(BulletType.GreenCrawler, new BulletTypeProperties(37, 4, false, spriteListSection(37, 2)));
        propertyList.Add(BulletType.CyanDot, new BulletTypeProperties(30, 4, false, null));
        propertyList.Add(BulletType.CyanDarkDot, new BulletTypeProperties(27, 4, false, null));
        propertyList.Add(BulletType.CyanShard, new BulletTypeProperties(32, 4, false, null));
        propertyList.Add(BulletType.CyanArrow, new BulletTypeProperties(19, 4, false, null));
        propertyList.Add(BulletType.CyanDarkArrow, new BulletTypeProperties(24, 4, false, null));
        propertyList.Add(BulletType.CyanCrawler, new BulletTypeProperties(22, 4, false, spriteListSection(22, 2)));
        propertyList.Add(BulletType.BlueDot, new BulletTypeProperties(11, 4, false, null));
        propertyList.Add(BulletType.BlueDarkDot, new BulletTypeProperties(8, 4, false, null));
        propertyList.Add(BulletType.BlueShard, new BulletTypeProperties(17, 4, false, null));
        propertyList.Add(BulletType.BlueArrow, new BulletTypeProperties(0, 4, false, null));
        propertyList.Add(BulletType.BlueDarkArrow, new BulletTypeProperties(5, 4, false, null));
        propertyList.Add(BulletType.BlueCrawler, new BulletTypeProperties(3, 4, false, spriteListSection(3, 2)));
        propertyList.Add(BulletType.BlueFire, new BulletTypeProperties(12, 4, true, spriteListSection(12, 4)));
        propertyList.Add(BulletType.PurpleDot, new BulletTypeProperties(76, 4, false, null));
        propertyList.Add(BulletType.PurpleDarkDot, new BulletTypeProperties(73, 4, false, null));
        propertyList.Add(BulletType.PurpleShard, new BulletTypeProperties(82, 4, false, null));
        propertyList.Add(BulletType.PurpleArrow, new BulletTypeProperties(65, 4, false, null));
        propertyList.Add(BulletType.PurpleDarkArrow, new BulletTypeProperties(70, 4, false, null));
        propertyList.Add(BulletType.PurpleCrawler, new BulletTypeProperties(68, 4, false, spriteListSection(68, 2)));
        propertyList.Add(BulletType.PurpleFire, new BulletTypeProperties(77, 4, true, spriteListSection(77, 4)));
        propertyList.Add(BulletType.PinkDot, new BulletTypeProperties(61, 4, false, null));
        propertyList.Add(BulletType.PinkDarkDot, new BulletTypeProperties(58, 4, false, null));
        propertyList.Add(BulletType.PinkShard, new BulletTypeProperties(63, 4, false, null));
        propertyList.Add(BulletType.PinkArrow, new BulletTypeProperties(50, 4, false, null));
        propertyList.Add(BulletType.PinkDarkArrow, new BulletTypeProperties(55, 4, false, null));
        propertyList.Add(BulletType.PinkCrawler, new BulletTypeProperties(68, 4, false, spriteListSection(68, 2)));
        propertyList.Add(BulletType.WhiteDot, new BulletTypeProperties(114, 4, false, null));
        propertyList.Add(BulletType.WhiteDarkDot, new BulletTypeProperties(111, 4, false, null));
        propertyList.Add(BulletType.WhiteShard, new BulletTypeProperties(116, 4, false, null));
        propertyList.Add(BulletType.WhiteArrow, new BulletTypeProperties(103, 4, false, null));
        propertyList.Add(BulletType.WhiteDarkArrow, new BulletTypeProperties(108, 4, false, null));
        propertyList.Add(BulletType.WhiteCrawler, new BulletTypeProperties(106, 4, false, spriteListSection(106, 2)));

        propertyList.Add(BulletType.RedOrb, new BulletTypeProperties(100, 16, false, null));
        propertyList.Add(BulletType.RedDarkOrb, new BulletTypeProperties(93, 16, false, null));
        propertyList.Add(BulletType.RedWave, new BulletTypeProperties(102, 16, false, null));
        propertyList.Add(BulletType.RedDarkWave, new BulletTypeProperties(94, 16, false, null));
        propertyList.Add(BulletType.RedBlade, new BulletTypeProperties(85, 16, false, null));
        propertyList.Add(BulletType.RedDarkBlade, new BulletTypeProperties(90, 16, false, null));
        propertyList.Add(BulletType.YellowOrb, new BulletTypeProperties(134, 16, false, null));
        propertyList.Add(BulletType.YellowDarkOrb, new BulletTypeProperties(127, 16, false, null));
        propertyList.Add(BulletType.YellowWave, new BulletTypeProperties(136, 16, false, null));
        propertyList.Add(BulletType.YellowDarkWave, new BulletTypeProperties(128, 16, false, null));
        propertyList.Add(BulletType.YellowBlade, new BulletTypeProperties(119, 16, false, null));
        propertyList.Add(BulletType.YellowDarkBlade, new BulletTypeProperties(124, 16, false, null));
        propertyList.Add(BulletType.GreenOrb, new BulletTypeProperties(46, 16, false, null));
        propertyList.Add(BulletType.GreenDarkOrb, new BulletTypeProperties(43, 16, false, null));
        propertyList.Add(BulletType.GreenWave, new BulletTypeProperties(48, 16, false, null));
        propertyList.Add(BulletType.GreenDarkWave, new BulletTypeProperties(44, 16, false, null));
        propertyList.Add(BulletType.GreenBlade, new BulletTypeProperties(35, 16, false, null));
        propertyList.Add(BulletType.GreenDarkBlade, new BulletTypeProperties(40, 16, false, null));
        propertyList.Add(BulletType.CyanOrb, new BulletTypeProperties(31, 16, false, null));
        propertyList.Add(BulletType.CyanDarkOrb, new BulletTypeProperties(28, 16, false, null));
        propertyList.Add(BulletType.CyanWave, new BulletTypeProperties(33, 16, false, null));
        propertyList.Add(BulletType.CyanDarkWave, new BulletTypeProperties(29, 16, false, null));
        propertyList.Add(BulletType.CyanBlade, new BulletTypeProperties(20, 16, false, null));
        propertyList.Add(BulletType.CyanDarkBlade, new BulletTypeProperties(25, 16, false, null));
        propertyList.Add(BulletType.BlueOrb, new BulletTypeProperties(16, 16, false, null));
        propertyList.Add(BulletType.BlueDarkOrb, new BulletTypeProperties(9, 16, false, null));
        propertyList.Add(BulletType.BlueWave, new BulletTypeProperties(18, 16, false, null));
        propertyList.Add(BulletType.BlueDarkWave, new BulletTypeProperties(10, 16, false, null));
        propertyList.Add(BulletType.BlueBlade, new BulletTypeProperties(1, 16, false, null));
        propertyList.Add(BulletType.BlueDarkBlade, new BulletTypeProperties(6, 16, false, null));
        propertyList.Add(BulletType.PurpleOrb, new BulletTypeProperties(81, 16, false, null));
        propertyList.Add(BulletType.PurpleDarkOrb, new BulletTypeProperties(74, 16, false, null));
        propertyList.Add(BulletType.PurpleWave, new BulletTypeProperties(83, 16, false, null));
        propertyList.Add(BulletType.PurpleDarkWave, new BulletTypeProperties(75, 16, false, null));
        propertyList.Add(BulletType.PurpleBlade, new BulletTypeProperties(66, 16, false, null));
        propertyList.Add(BulletType.PurpleDarkBlade, new BulletTypeProperties(71, 16, false, null));
        propertyList.Add(BulletType.PinkOrb, new BulletTypeProperties(62, 16, false, null));
        propertyList.Add(BulletType.PinkDarkOrb, new BulletTypeProperties(74, 16, false, null));
        propertyList.Add(BulletType.PinkWave, new BulletTypeProperties(64, 16, false, null));
        propertyList.Add(BulletType.PinkDarkWave, new BulletTypeProperties(60, 16, false, null));
        propertyList.Add(BulletType.PinkBlade, new BulletTypeProperties(51, 16, false, null));
        propertyList.Add(BulletType.PinkDarkBlade, new BulletTypeProperties(56, 16, false, null));
        propertyList.Add(BulletType.WhiteOrb, new BulletTypeProperties(115, 16, false, null));
        propertyList.Add(BulletType.WhiteDarkOrb, new BulletTypeProperties(112, 16, false, null));
        propertyList.Add(BulletType.WhiteWave, new BulletTypeProperties(117, 16, false, null));
        propertyList.Add(BulletType.WhiteDarkWave, new BulletTypeProperties(113, 16, false, null));
        propertyList.Add(BulletType.WhiteBlade, new BulletTypeProperties(104, 16, false, null));
        propertyList.Add(BulletType.WhiteDarkBlade, new BulletTypeProperties(109, 16, false, null));

        propertyList.Add(BulletType.RedBubble, new BulletTypeProperties(86, 64, false, null));
        propertyList.Add(BulletType.RedDarkBubble, new BulletTypeProperties(91, 64, false, null));
        propertyList.Add(BulletType.YellowBubble, new BulletTypeProperties(120, 64, false, null));
        propertyList.Add(BulletType.YellowDarkBubble, new BulletTypeProperties(125, 64, false, null));
        propertyList.Add(BulletType.GreenBubble, new BulletTypeProperties(36, 64, false, null));
        propertyList.Add(BulletType.GreenDarkBubble, new BulletTypeProperties(41, 64, false, null));
        propertyList.Add(BulletType.CyanBubble, new BulletTypeProperties(21, 64, false, null));
        propertyList.Add(BulletType.CyanDarkBubble, new BulletTypeProperties(26, 64, false, null));
        propertyList.Add(BulletType.BlueBubble, new BulletTypeProperties(2, 64, false, null));
        propertyList.Add(BulletType.BlueDarkBubble, new BulletTypeProperties(7, 64, false, null));
        propertyList.Add(BulletType.PurpleBubble, new BulletTypeProperties(67, 64, false, null));
        propertyList.Add(BulletType.PurpleDarkBubble, new BulletTypeProperties(72, 64, false, null));
        propertyList.Add(BulletType.PinkBubble, new BulletTypeProperties(52, 64, false, null));
        propertyList.Add(BulletType.PinkDarkBubble, new BulletTypeProperties(57, 64, false, null));
        propertyList.Add(BulletType.WhiteBubble, new BulletTypeProperties(105, 64, false, null));
        propertyList.Add(BulletType.WhiteDarkBubble, new BulletTypeProperties(110, 64, false, null));
    }

    Sprite[] spriteListSection(int start, int size) {
        Sprite[] section = new Sprite[size];
        for(int i = 0; i < size; i++) {
            section[i] = spriteList[start + i];
        }
        return section;
    }

    // Shoots a bullet from the specified position, with a certain speed and angle. The bullet is stored for subsequent AddActions.
    public static void ShootBullet(Vector2 position, float speed, float angle, BulletType type)
    {
        GameObject bt = bulletPool.GetRecycledObject();
        bt.SetActive(true);
        bt.GetComponent<Bullet>().Init(position, speed, angle);
        bt.GetComponent<Bullet>().SetGraphic(propertyList[type]);
        shotID = bt;
    }

    // Shoots a bullet with a few more parameters. The bullet is stored for subsequent AddActions.
    public static void ShootBullet(Vector2 position, float speed, float angle, float acc, float max, float angv, BulletType type)
    {
        GameObject bt = bulletPool.GetRecycledObject();
        bt.SetActive(true);
        bt.GetComponent<Bullet>().Init(position, speed, angle, acc, max, angv);
        bt.GetComponent<Bullet>().SetGraphic(propertyList[type]);
        shotID = bt;
    }

    // Adds an action to the queue of the last shot bullet.
    public static void AddAction(BulletAction action)
    {
        shotID.GetComponent<Bullet>().AddAction(action);
    }

    // Adds a list of actions to the queue of the last shot bullet.
    public static void AddAction(List<BulletAction> actions)
    {
        foreach(BulletAction action in actions)
        {
            shotID.GetComponent<Bullet>().AddAction(action);
        }
    }

    // Adds an action to the queue of a specified bullet.
    public static void AddAction(GameObject shot, BulletAction action)
    {
        shot.GetComponent<Bullet>().AddAction(action);
    }

    // Adds a list of actions to the queue of a specified bullet.
    public static void AddAction(GameObject shot, List<BulletAction> actions)
    {
        foreach (BulletAction action in actions)
        {
            shot.GetComponent<Bullet>().AddAction(action);
        }
    }

    // Deletes the specified bullet.
    public static void DeleteBullet(GameObject shot)
    {
        shot.GetComponent<Bullet>().Reset();
        bulletPool.Recycle(shot);
    }

    public static float AngleToPlayerFrom(Vector2 t) {
        if (duplicateExists)
        {
            Vector2 pt = duplicateObj.transform.position;
            return Mathf.Rad2Deg * Mathf.Atan2(pt.y - t.y, pt.x - t.x);
        }
        else
        {
            Vector2 pt = GameObject.FindGameObjectWithTag("Player").transform.position;
            return Mathf.Rad2Deg * Mathf.Atan2(pt.y - t.y, pt.x - t.x);
        }
    }
}

public class BulletTypeProperties {
    public Sprite graphicIndex;
    public int radius;
    public bool isAddBlend;
    public Sprite[] extraAnimation;

    public BulletTypeProperties(int g, int r, bool add, Sprite[] ani) {
        graphicIndex = BulletManager.spriteList[g];
        radius = r;
        isAddBlend = add;
        extraAnimation = ani;
    }
}

public enum BulletType {
    RedDot, RedDarkDot, RedShard, RedArrow, RedDarkArrow, RedCrawler, RedFire,
    YellowDot, YellowDarkDot, YellowShard, YellowArrow, YellowDarkArrow, YellowCrawler, YellowFire,
    GreenDot, GreenDarkDot, GreenShard, GreenArrow, GreenDarkArrow, GreenCrawler,
    CyanDot, CyanDarkDot, CyanShard, CyanArrow, CyanDarkArrow, CyanCrawler,
    BlueDot, BlueDarkDot, BlueShard, BlueArrow, BlueDarkArrow, BlueCrawler, BlueFire,
    PurpleDot, PurpleDarkDot, PurpleShard, PurpleArrow, PurpleDarkArrow, PurpleCrawler, PurpleFire,
    PinkDot, PinkDarkDot, PinkShard, PinkArrow, PinkDarkArrow, PinkCrawler,
    WhiteDot, WhiteDarkDot, WhiteShard, WhiteArrow, WhiteDarkArrow, WhiteCrawler,

    RedOrb, RedDarkOrb, RedWave, RedDarkWave, RedBlade, RedDarkBlade,
    YellowOrb, YellowDarkOrb, YellowWave, YellowDarkWave, YellowBlade, YellowDarkBlade,
    GreenOrb, GreenDarkOrb, GreenWave, GreenDarkWave, GreenBlade, GreenDarkBlade,
    CyanOrb, CyanDarkOrb, CyanWave, CyanDarkWave, CyanBlade, CyanDarkBlade,
    BlueOrb, BlueDarkOrb, BlueWave, BlueDarkWave, BlueBlade, BlueDarkBlade,
    PurpleOrb, PurpleDarkOrb, PurpleWave, PurpleDarkWave, PurpleBlade, PurpleDarkBlade,
    PinkOrb, PinkDarkOrb, PinkWave, PinkDarkWave, PinkBlade, PinkDarkBlade,
    WhiteOrb, WhiteDarkOrb, WhiteWave, WhiteDarkWave, WhiteBlade, WhiteDarkBlade,

    RedBubble, RedDarkBubble,
    YellowBubble, YellowDarkBubble,
    GreenBubble, GreenDarkBubble,
    CyanBubble, CyanDarkBubble,
    BlueBubble, BlueDarkBubble,
    PurpleBubble, PurpleDarkBubble,
    PinkBubble, PinkDarkBubble,
    WhiteBubble, WhiteDarkBubble
}