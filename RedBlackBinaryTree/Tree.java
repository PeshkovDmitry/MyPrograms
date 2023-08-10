public interface Tree<T extends Comparable<T>> {

    void add(T value);

    Node<T> getRoot();

    void printTree();

}
