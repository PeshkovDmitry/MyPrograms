public class ComplexValue implements Complex {

    private double realPart;

    private double imagePart;

    private double amplitude;

    private double angle;

    public ComplexValue() {}

    public ComplexValue(String value) {
        value = value.replace(" ","")
                .replace("i","j")
                .replace("+j","j+")
                .replace("-j","j-")
                .trim();
        if (!value.contains("j")) {
            setInGeneralForm(Double.parseDouble(value),0);
        }
        else {
            String parts[] = value.split("j");
            if (parts.length == 1) {
                setInGeneralForm(0, Double.parseDouble(parts[0]));
            }
            else if (parts.length == 2) {
                setInGeneralForm(Double.parseDouble(parts[0]), Double.parseDouble(parts[1]));
            }
        }
    }

    @Override
    public double getRealValue() {
        return realPart;
    }

    @Override
    public double getImageValue() {
        return imagePart;
    }

    @Override
    public double getAmplitude() {
        return amplitude;
    }

    @Override
    public double getAngle() {
        return angle;
    }

    @Override
    public void setInGeneralForm(double realPart, double imagePart) {
        this.realPart = realPart;
        this.imagePart = imagePart;
        this.amplitude = Math.sqrt(realPart * realPart + imagePart * imagePart);
        this.angle = Math.atan(imagePart/realPart);
    }

    @Override
    public void setInPolarForm(double amplitude, double angle) {
        this.realPart = amplitude * Math.cos(angle);
        this.imagePart = amplitude * Math.sin(angle);
        this.amplitude = amplitude;
        this.angle = angle;
    }

    @Override
    public String toString() {
        String out = String.format("%.3f", getRealValue());
        if (getImageValue() < 0) {
            out += "-j" + String.format("%.3f", Math.abs(getImageValue()));
        }
        else if (getImageValue() > 0) {
            out += "+j" + String.format("%.3f", getImageValue());
        }
        return out;
    }
}
