import java.util.Random;

public class Main {
    public static void main(String[] args) {
        Tree<Integer> myTree = new BinaryTree<>();
        for (int i = 0; i < 20; i++) {
            myTree.add(new Random().nextInt(20));
        }
        BinaryTreePrinter printer = new BinaryTreePrinter();
        printer.print(myTree);
    }
}