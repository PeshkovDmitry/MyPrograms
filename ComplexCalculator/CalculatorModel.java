public class CalculatorModel implements Model{
    Complex result;

    @Override
    public void add(Complex number1, Complex number2) {
        result = new ComplexValue();
        result.setInGeneralForm(
                number1.getRealValue() + number2.getRealValue(),
                number1.getImageValue() + number2.getImageValue()
        );
    }

    @Override
    public void sub(Complex number1, Complex number2) {
        result = new ComplexValue();
        result.setInGeneralForm(
                number1.getRealValue() - number2.getRealValue(),
                number1.getImageValue() - number2.getImageValue()
        );
    }

    @Override
    public void mult(Complex number1, Complex number2) {
        result = new ComplexValue();
        result.setInPolarForm(
                number1.getAmplitude() * number2.getAmplitude(),
                number1.getAngle() + number2.getAngle()
        );
    }

    @Override
    public void div(Complex number1, Complex number2) {
    result = new ComplexValue();
        result.setInPolarForm(
                number1.getAmplitude() / number2.getAmplitude(),
                number1.getAngle() - number2.getAngle()
                );
    }

    @Override
    public Complex getResult() {
        return result;
    }
}
