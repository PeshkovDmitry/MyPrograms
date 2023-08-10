public class CheckingCalculatorModel extends CalculatorModel {

    @Override
    public void div(Complex number1, Complex number2) {
        if (number2.getAmplitude() != 0) {
            super.div(number1, number2);
        }
        else {
            throw new IllegalArgumentException("На ноль делить нельзя!");
        }
    }

}
