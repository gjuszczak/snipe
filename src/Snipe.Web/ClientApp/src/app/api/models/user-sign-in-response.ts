/* tslint:disable */
/* eslint-disable */
import { SignInFailReason } from './sign-in-fail-reason';
export interface UserSignInResponse {
  accessToken?: null | string;
  failReason?: SignInFailReason;
  lockoutExpiration?: null | string;
  refreshToken?: null | string;
  success?: boolean;
}
