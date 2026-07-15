export interface IUser {
  id: number;
  username: string;
  email: string;
  fullName: string;
}

export interface IAuthResponse {
  token: string;
  expiration: string;
  user: IUser;
}