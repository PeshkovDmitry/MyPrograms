public interface Node<T extends Comparable<T>> {
    T getValue();

    void setValue(T value);

    Color getColor();

    void setColor(Color color);

    Node getLeftChild();

    void setLeftChild(Node leftChild);

    Node getRightChild();

    void setRightChild(Node rightChild);

}
