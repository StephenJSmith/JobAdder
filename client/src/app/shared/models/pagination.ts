export interface IPagination<T> {
  pageNumber: number;
  pageSize: number;
  count: number;
  items: T[];
}
