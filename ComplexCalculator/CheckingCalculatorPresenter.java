public class CheckingCalculatorPresenter extends CalculatorPresenter {
    public CheckingCalculatorPresenter(Model model, View view) {
        super(model, view);
    }

    @Override
    public void onButtonClicked() {
        try {
            super.onButtonClicked();
        }
        catch (IllegalArgumentException e) {
            System.out.println(e.getMessage());
        }
    }


}
