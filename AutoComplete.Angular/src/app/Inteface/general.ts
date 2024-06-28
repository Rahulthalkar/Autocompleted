export interface General {}

export interface APIResult<T> {
  value: T;
  isSuccess: boolean;
  errorMessageKey: string;
  exceptionInfo: string;
}

export interface RolesAndPermissions {
  permissions: {
    [key: string]: string[];
  };
}

export interface NotificationModel {
  notificationFromUserId: number;
  notificationToUserId: number;
  sentOn: Date | null;
  deviceId: string | null;
  deviceType: string | null;
  title: string | null;
  message: string | null;
}

export interface Login {
  email: string;
  password: string;
}
export interface LoginResponse{
  id:number;
  fullName:string;
  userType:string;
  userTypeId:string
}
