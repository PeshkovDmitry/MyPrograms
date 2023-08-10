import java.util.Scanner;

public class CheckingCalculatorView extends CalculatorView {

    public CheckingCalculatorView(Scanner scanner) {
        super(scanner);
    }

    @Override
    public String getUserInput(String message) {
        String op =  super.getUserInput(message);
        if (message.equals(Messages.INPUT_OPERATION)) {
            if (op.matches("\\/|\\*|\\+|-")) {
                return op;
            }
            else {
                throw new IllegalArgumentException("Операция должна быть +, -, / или *!");
            }
        }
        return op;
    }


}
