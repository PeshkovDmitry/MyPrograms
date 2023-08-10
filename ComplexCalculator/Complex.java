public interface Complex {

    double getRealValue();

    double getImageValue();

    double getAmplitude();

    double getAngle();

    void setInGeneralForm(double realPart, double imagePart);

    void setInPolarForm(double amplitude, double angle);

}
