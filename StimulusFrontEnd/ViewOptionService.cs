namespace StimulusFrontEnd
{
    public class ViewOptionService
    {
        private bool _navBarVisible = true;

        public Action OnChanged { get; set; }

        //Change l'état en cliwuant sur le bouton dans PageNoued
        public void Toggle()
        {
            _navBarVisible = !_navBarVisible;//Change si le navMenu est visible ou non
            if (OnChanged != null) OnChanged();//Callback pour reload
        }

        //Css additionnel pour pouvoir cacher le navMenu
        public string NavBarClass
        {
            get
            {
                if (_navBarVisible) return "d-none"; //Cache le "div" si il est visible de base
                return String.Empty; //Ne fait rien si il n'est pas visible de base
            }
        }
    }
}
