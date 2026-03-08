using UnityEditor.Search;
using UnityEngine;

public class AttackPresenter : MonoBehaviour
{
    [SerializeField] PlayerAttacks playerAttacks;

    [SerializeField] GameObject freezePresenter;
    [SerializeField] GameObject gravityPresenter;

    private void Update()
    {
        Attacks currentAttack = playerAttacks.GetSelectedAbility();

        if (currentAttack == null)
        {
            HideAll();
            return;
        }

        if (currentAttack is FreezeAttack freeze) 
        {
            ShowFreeze(freeze);
        }

        else if (currentAttack is GravityPushAttack gravity)
        {
            ShowGravity(gravity);
        }

        else
            HideAll();
    }

    void ShowFreeze(FreezeAttack freeze)
    {
        freezePresenter.SetActive(true);
        gravityPresenter.SetActive(false);

        float diameter = freeze.range;
        float angle = freeze.angle / 360;
        freezePresenter.transform.localScale = new Vector3(diameter * angle, 0.01f, diameter);
    }

    void ShowGravity(GravityPushAttack gravity)
    {
        freezePresenter.SetActive(false);
        gravityPresenter.SetActive(true);

        float diameter = gravity.radius * 2 ;
        gravityPresenter.transform.localScale = new Vector3(diameter, 0.01f, diameter);
    }

    void HideAll()
    {
        freezePresenter.SetActive(false);
        gravityPresenter.SetActive(false);
    }
}
