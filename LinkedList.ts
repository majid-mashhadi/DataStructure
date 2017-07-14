export class LinkedListNode<T> {
    public data: T;
    private next: LinkedListNode<T>;
    private prev: LinkedListNode<T>;
    constructor(data: T) {
        this.data = data;
    }

    get Next(): LinkedListNode<T> {
        return this.next;
    }

    set Next(node: LinkedListNode<T>) {
        this.next = node;
    }

    get Prev(): LinkedListNode<T> {
        return this.prev;
    }
    set Prev(node: LinkedListNode<T>) {
        this.prev = node;
    }
}

export class LinkedList<T> {
    public head: LinkedListNode<T>;
    public tail: LinkedListNode<T>;
    //constructor(data: T) {
    //    this.addFirst(data);
    //}

    public addFirst = function (data: T) {
        var node: LinkedListNode<T>;
        node = new LinkedListNode(data);
        if (this.head == null) {
            this.head = node;
            this.tail = this.head;
        }
        else {
            node.Next = this.head;
            this.head.prev = node;
            this.head = node;
        }
    }

    public addLast = function (data: T) {
        if (this.head == null) {
            this.addFirst(data);
            return;
        }
        var node: LinkedListNode<T>;
        node = new LinkedListNode(data);
        this.tail.next = node;
        node.Prev = this.tail;
        this.tail = node;
    }

    remove = function (data: T) {
        var current: LinkedListNode<T> = this.head;
        while (current != null) {
            if (current.data == data) {
                if (current == this.head) {
                    if (this.head == this.tail) {
                        this.head = null;
                        this.tail = null;
                    }
                    else {
                        current.Next.Prev = null;
                        this.head = current.Next;
                    }
                    return;
                }
                else if (current == this.tail) {
                    if (this.head == this.tail) {
                        this.head = null;
                        this.tail = null;
                    }
                    else {
                        current.Prev.Next = null;
                        this.tail = current.Prev;
                    }
                    return;
                }
                else {
                    current.Prev.Next = current.Next;
                    current.Next.Prev = current.Prev;
                    return;
                }
            }
            current = current.Next;
        }
    }

    public print(): void {
        var current: LinkedListNode<T> = this.head;
        while (current != null) {
            console.log(String(current.data));
            current = current.Next;
        }
    }
}
