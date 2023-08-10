public interface Printer<T extends Comparable<T>> {

    void print(Tree<T> tree);

}
