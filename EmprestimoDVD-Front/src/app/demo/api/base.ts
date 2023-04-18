export interface base<T> {
    code: number;
    message: string;
    date: Date;
    object: T;
    error: {
        message: string;
        innerMessage: string;
        stackTrace: string;
    };
    elapsed: {
        elapsed: string;
        elapsedMilliseconds: number;
    };
}

export interface itemDropdown {
    code: number;
    name: string;
}
