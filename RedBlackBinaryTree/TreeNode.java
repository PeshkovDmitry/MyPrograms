public class TreeNode<T extends Comparable<T>> implements Node<T> {

    private T value;

    private Color color;

    private Node leftChild;

    private Node rightChild;

    public TreeNode() {}

    public TreeNode(T value) {
        this.value = value;
    }
    @Override
    public T getValue() {
        return value;
    }

    @Override
    public void setValue(T value) {
        this.value = value;
    }

    @Override
    public Color getColor() {
        return color;
    }

    @Override
    public void setColor(Color color) {
        this.color = color;
    }

    @Override
    public Node getLeftChild() {
        return leftChild;
    }

    @Override
    public void setLeftChild(Node leftChild) {
        this.leftChild = leftChild;
    }

    @Override
    public Node getRightChild() {
        return rightChild;
    }

    @Override
    public void setRightChild(Node rightChild) {
        this.rightChild = rightChild;
    }

    @Override
    public String toString() {
        return String.format("[%s (%s): левый потомок %s (%s), правый потомок %s (%s)]",
                value,
                color,
                leftChild != null ? leftChild.getValue() : "-",
                leftChild != null ? leftChild.getColor() : "-",
                rightChild != null ? rightChild.getValue() : "-",
                rightChild != null ? rightChild.getColor() : "-"
            );

    }
}
