export interface User {
  userId: number;
  fullName: string;
  email: string;
  idRole: number;
  roleName?: string;
  password: string;
  passwordHash?: string;
  isActive: number;
}
