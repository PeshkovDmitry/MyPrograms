import java.util.logging.FileHandler;
import java.util.logging.Logger;
import java.util.logging.SimpleFormatter;

public class CalculatorLogger {
    private static CalculatorLogger calculatorLogger;

    private static Logger logger;

    private CalculatorLogger() {
        String logpath = "log.txt";
        logger = Logger.getAnonymousLogger();
        FileHandler fileHandler = null;
        try {
            fileHandler = new FileHandler(logpath, true);
            fileHandler.setEncoding("UTF-8");
            logger.addHandler(fileHandler);
            SimpleFormatter simpleFormatter = new SimpleFormatter();
            fileHandler.setFormatter(simpleFormatter);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static CalculatorLogger getInstance() {
        if (calculatorLogger == null) {
            calculatorLogger = new CalculatorLogger();
        }
        return calculatorLogger;
    }

    public void log(String message) {
        logger.info(message);
    }

    public void close() {
        logger.getHandlers()[0].close();
    }
}
