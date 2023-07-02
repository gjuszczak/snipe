import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenStoreService {
  private accessToken: string | null = "test123";

  public getAccessToken() : string | null {
    return this.accessToken;
  }

  public setAccessToken(accessToken: string) : void {
    this.accessToken = accessToken;
  }

  public clearAccessToken() : void {
    this.accessToken = null;
  }

  public getRefreshToken() : string | null { 
    return localStorage.getItem("refreshToken");
  }

  public setRefreshToken(refreshToken: string) : void {
    localStorage.setItem("refreshToken", refreshToken);
  }

  public clearRefreshToken() : void {
    localStorage.removeItem("refreshToken");
  }
}