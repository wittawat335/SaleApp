import { SalesDetail } from './sales-detail';

export interface Sale {
  saleId?: number;
  documentNumber?: string;
  paymentType: string;
  totalText: string;
  recordDate?: string;
  saleDetails: SalesDetail[];
}
