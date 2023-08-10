import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Model model = new CheckingCalculatorModel();
        View view = new CheckingCalculatorView(new Scanner(System.in));
        Presenter presenter = new CheckingCalculatorPresenter(model, view);
        presenter.onButtonClicked();
        CalculatorLogger.getInstance().close();
    }
}