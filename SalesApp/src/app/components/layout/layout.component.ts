import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Menu } from 'src/app/shared/interfaces/menu';
import { MenuService } from 'src/app/shared/services/menu.service';
import { UtilityService } from 'src/app/shared/utility/utility.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
})
export class LayoutComponent implements OnInit {
  listMenus: Menu[] = [];
  userName: string = '';
  roleUser: string = '';

  constructor(
    private router: Router,
    private menuService: MenuService,
    private utService: UtilityService
  ) {}

  ngOnInit(): void {
    this.getMenu();
  }
  getMenu() {
    const user = this.utService.getSessionUser();
    console.log(user);
    if (user != null) {
      this.userName = user.fullName;
      this.roleUser = user.roleName;

      this.menuService.GetList(user.userId).subscribe({
        next: (data) => {
          if (data.status) this.listMenus = data.value;
        },
        error: (e) => {},
      });
    }
  }

  logOut() {
    this.utService.removeSessionUser();
    this.router.navigate(['login']);
  }
}
