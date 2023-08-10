import java.util.*;

public class BinaryTreePrinter<T extends Comparable<T>> implements Printer{

     @Override
    public void print(Tree tree) {
        List<String> list = getPlainList(tree);
        int maxValueLength = getMaxLength(list) + 2;
        int depth = getTreeDepth(tree.getRoot(),0);
        List<String> lines = getTreeAsString(list, depth, maxValueLength);
        cutHorizontal(lines);
        markColor(lines);
        for (String line: lines) {
            System.out.println(line);
        }
    }

    private void markColor(List<String> lines) {
        for (int i = 0; i < lines.size(); i++) {
            lines.set(i, lines.get(i).replace("{",ANSI_RED + " "));
            lines.set(i, lines.get(i).replace("}",ANSI_RESET + " "));
        }
    }

    /**
     * Метод, сжимающий дерево по горизонтали
     * @param lines
     */
    private void cutHorizontal(List<String> lines) {
        List<List<String>> newLines = new ArrayList<>();
        for (int i = 0; i < lines.size() - 1; i++) {
            newLines.add(new ArrayList<>());
        }
        for (int i = 0; i < lines.get(0).length(); i++) {
            boolean canDelete = true;
            for (int j = 0; j < lines.size() - 1; j++) {
                canDelete = canDelete && !(lines.get(j).charAt(i) != ' ' && lines.get(j).charAt(i) != '-');
            }
            if (!canDelete) {
                for (int j = 0; j < lines.size() - 1; j++) {
                    newLines.get(j).add(lines.get(j).substring(i,i + 1));
                }
            }
        }
        for (int i = 0; i < newLines.size(); i++) {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < newLines.get(i).size(); j++) {
                if ((newLines.get(i).get(j).equals("┌") || newLines.get(i).get(j).equals("┐"))
                        && (i + 1 == newLines.size() || newLines.get(i + 1).get(j).equals(" "))) {
                    sb.append(" ");
                }
                else {
                    sb.append(newLines.get(i).get(j));
                }
            }
            lines.set(i, sb.toString());
        }
        lines.remove(newLines.size());
    }

    /**
     * Метод, формирующий список строк с изображением дерева
     * @param list
     * @param depth
     * @param maxValueLength
     * @return
     */

    private List<String> getTreeAsString(List<String> list, int depth, int maxValueLength) {
        int level = 0;
        int nextNewLine = 0;
        List<String> lines = new ArrayList<>();
        lines.add("");
        for (int i = 0; i < list.size(); i++) {
            int currentLength = (int) (maxValueLength * Math.pow(2, depth - level - 1));
            lines.set(level, lines.get(level) + getFormattedString(list.get(i),currentLength));
            if (i == nextNewLine) {
                level++;
                lines.add("");
                nextNewLine += Math.pow(2, level);
            }
        }
        return lines;
    }

    /**
     * Метод, возвращающий длину строки максимального значения
     * @param list
     * @return
     */

    private int getMaxLength(List<String> list) {
        int maxValueLength = 0;
        for (String s: list) {
            if (s.length() > maxValueLength) {
                maxValueLength = s.length();
            }
        }
        return maxValueLength;
    }

    /**
     * Метод, возвращающий дерево в виде последовательного списка значений узлов.
     * Последовательность повторяет просмотр по ширине.
     * При этом отсутствующие узлы также добавляются в виде пустой строки.
     * @param tree
     * @return
     */

    private List<String> getPlainList(Tree tree) {
        List<String> list = new ArrayList<>();
        Deque<Node<T>> deque = new LinkedList<>();
        deque.add(tree.getRoot());
        int depth = getTreeDepth(tree.getRoot(),0);
        boolean run = true;
        while (deque.size() > 0 && run) {
            Node<T> currentNode = deque.pollFirst();
            String text = "";
            if (currentNode != null) {
                text = currentNode.getValue().toString();
                deque.add(currentNode.getLeftChild());
                deque.add(currentNode.getRightChild());
                if (currentNode.getColor() == Color.RED) {
                    text = "{" + text + "}";
                }
            }
            else {
                deque.add(null);
                deque.add(null);
            }
            list.add(text);
            run = (Math.pow(2, depth) != list.size() + 1);
        }
        return list;
    }

    /**
     * Метод для получения глубины дерева
     * @param node
     * @param currentDepth
     * @return
     */

    private int getTreeDepth(Node<T> node, int currentDepth) {
        if (node == null) {
            return currentDepth;
        }
        if (node.getLeftChild() == null && node.getRightChild() == null) {
            return currentDepth + 1;
        }
        return currentDepth + 1 +
                Math.max(
                    getTreeDepth(node.getLeftChild(), currentDepth),
                    getTreeDepth(node.getRightChild(), currentDepth)
                );
    }

    /**
     * Метод, формирующий строку заданной длины и ветками
     * @param text
     * @param fieldsize
     * @return
     */

    private String getFormattedString(String text, int fieldsize) {
        StringBuilder sb = new StringBuilder();
        if (text == "") {
            return " ".repeat(fieldsize);
        }
        if (fieldsize == text.length() + 2) {
            return " " + text + " ";
        }
        int spaceBefore = (fieldsize - text.length()) / 2;
        int spaceAfter = fieldsize - text.length() - spaceBefore;
        int spaceBefore1 = spaceBefore / 2;
        int spaceBefore2 = spaceBefore - spaceBefore1 - 1;
        int spaceAfter1 = spaceAfter / 2;
        int spaceAfter2 = spaceAfter - spaceAfter1 - 1;
        sb.append(" ".repeat(spaceBefore1));
        sb.append("┌");
        sb.append("-".repeat(spaceBefore2));
        sb.append(text);
        sb.append("-".repeat(spaceAfter1));
        sb.append("┐");
        sb.append(" ".repeat(spaceAfter2));
        return sb.toString();
    }

    /**
     * Метод, возвращающий заданную строку, повторенную заданное число раз
     * @param text
     * @param count
     * @return
     */

    private String ANSI_RESET = "\u001B[0m";

    private String ANSI_RED = "\u001B[31m";

}
