public class CalculatorPresenter implements Presenter{
    private Model model;
    private View view;

    public CalculatorPresenter(Model model, View view) {
        this.model = model;
        this.view = view;
    }

    @Override
    public void onButtonClicked() {
        Complex number1 = new ComplexValue(view.getUserInput(Messages.INPUT_FIRST_VALUE));
        Complex number2 = new ComplexValue(view.getUserInput(Messages.INPUT_SECOND_VALUE));
        String operation = view.getUserInput(Messages.INPUT_OPERATION);
        switch (operation) {
            case "+":
                model.add(number1, number2);
                break;
            case "-":
                model.sub(number1, number2);
                break;
            case "*":
                model.mult(number1, number2);
                break;
            case "/":
                model.div(number1, number2);
                break;
        }
        Complex result = model.getResult();
        CalculatorLogger.getInstance().log("(" + number1 + ")" + operation + "(" + number2 + ") = " + result);
        view.displayResult(result);
    }

}
