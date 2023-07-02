import { Component, OnInit } from '@angular/core';
import { TokenStoreService } from '../services/token-store.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html'
})
export class LogoutComponent implements OnInit {

  constructor(
    private authService: TokenStoreService) { }

  ngOnInit() {
    //this.authService.logout();
  }
}