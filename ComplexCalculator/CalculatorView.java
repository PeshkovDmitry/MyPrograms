import java.util.Scanner;

public class CalculatorView implements View {

    private Scanner scanner;

    public CalculatorView(Scanner scanner) {
        this.scanner = scanner;
    }

    public void displayResult(Complex result) {
        System.out.println(result);
    }

    public String getUserInput(String message) {
        System.out.print(message);
        return scanner.next();
    }
}
