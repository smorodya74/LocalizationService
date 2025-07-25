export interface PageResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
}