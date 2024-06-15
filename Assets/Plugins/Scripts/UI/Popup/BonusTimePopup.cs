using DG.Tweening;
using H2910.UI.Popups;

public class BonusTimePopup : BasePopUp
{

    private Tween _tween;
    
    public void Close()
    {
        if (OnClick)
            return;
        BlockMultyClick();
        OnCloseScreen();
        _tween?.Kill();
    }
}
