import { LinkedList } from './LinkedList'
import { KeyValuePair } from './Objects'

export class Dictionary<Tkey, Tvalue> {

    private size: number;
    //Array<LinkedList<number>> = [];
    items: Array<LinkedList<[Tkey, Tvalue]>> = [];
    constructor(size: number) {
        this.size = size || 100;
        this.items = new Array(this.size);
    }

    getHash(key: any): number {
        if (typeof key == "object") {
            key = JSON.stringify(key);
            console.log(key);
        }
        if (typeof key == "number") {
            return key;
        }
        else if (typeof key == "string") {
            var hash1: number;
            hash1 = (5381 << 16) + 5381;
            var hash2: number = hash1;
            var len: number = key.length;
            var pint1: number = len - 1;
            var pint2: number = pint1;
            while (len > 0) {
                pint2 = pint1;
                if (pint1 > 0) pint2 = pint1 - 1;
                hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ key.charCodeAt(pint1);
                hash2 = ((hash2 << 5) + hash2 + (hash2 >> 27)) ^ key.charCodeAt(pint2);
                len -= 2;
                pint1 -= 2;
            }
            if (len > 0) {
                hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ key.charCodeAt(1);
                hash2 = ((hash2 << 5) + hash2 + (hash2 >> 27)) ^ key.charCodeAt(0);
            }

            return hash1 + (hash2 * 1566083941);
        }
        else {
            return 1;
        }
    }

    private getIndex(key: any): number {
        return Math.abs(this.getHash(key) % this.size);
    }

    containsKey = function (key: Tkey): boolean {
        var index: number = this.getIndex(key);
        var item: LinkedList<KeyValuePair<Tkey, Tvalue>>;
        var obj: KeyValuePair<Tkey, Tvalue>;
        if (this.items[index] != null) {
            item = this.items[index];
            var current = item.head;
            while (current != null) {
                if (current.data.key == key) {
                    return true;
                }
                current = current.Next;
            }
        }
        return false;
    }
    containsValue = function (value: Tvalue): boolean {
        for (var index = 0; index < this.size; index++) {
            var item: LinkedList<KeyValuePair<Tkey, Tvalue>> = this.items[index];
            if (item != null) {
                var current = item.head;
                while (current != null) {
                    if (current.data.value == value)
                        return true;
                    current = current.Next;
                }
            }
        }
        return false;
    }

    add = function (key: Tkey, value: Tvalue) {
        if (this.containsKey(key)) return;
        var index: number = this.getIndex(key);
        var item: LinkedList<KeyValuePair<Tkey, Tvalue>>;
        var obj: KeyValuePair<Tkey, Tvalue> = new KeyValuePair(key, value);
        if (this.items[index] == null) {
            item = new LinkedList<KeyValuePair<Tkey, Tvalue>>();
            item.addFirst(obj);
            this.items[index] = item;
        }
        else {
            item = this.items[index];
            var current = item.head;
            while (current != null) {
                if (current.data.key == key) return;
                current = current.Next;
            }
            item.addFirst(obj);
        }
    }

    remove = function (key: Tkey) {
        var index: number = this.getIndex(key);
        var item: LinkedList<KeyValuePair<Tkey, Tvalue>>;
        var obj: KeyValuePair<Tkey, Tvalue>;
        if (this.items[index] != null) {
            item = this.items[index];
            var current = item.head;
            while (current != null) {
                if (current.data.key == key) {
                    item.remove(current.data);
                    return;
                }
                current = current.Next;
            }
        }
    }

    clear = function () {
        for (var index = 0; index < this.size; index++) {
            this.items[index] = null;
        }
    }

    print = function (): void {
        for (var index = 0; index < this.size; index++) {
            var item: LinkedList<KeyValuePair<Tkey, Tvalue>> = this.items[index];
            if (item != null) {
                console.log("Items at index: " + index.toString())
                var current = item.head;
                while (current != null) {
                    console.log(current.data.key);
                    current = current.Next;
                }
            }
        }
    }
}