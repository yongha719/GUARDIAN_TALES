namespace GUARDIANTALES
{
    public class Garam : Guardian
    {
        protected override void Attack()
        {
            if (Weapon != null)
                Weapon.Attack();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}