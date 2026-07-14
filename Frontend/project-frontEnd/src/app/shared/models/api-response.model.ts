 export interface IApiResponse {
  data: TimeRanges;
  isSuccess: boolean;
  message: string;
  errors?: string[];
}