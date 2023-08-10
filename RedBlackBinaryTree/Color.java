public enum Color {
    RED ("Красный"),
    BLACK ("Чёрный");

    private String title;

    Color(String title) {
        this.title = title;
    }

    @Override
    public String toString() {
        return title;
    }
}
