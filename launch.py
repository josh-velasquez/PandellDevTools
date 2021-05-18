import subprocess
import webbrowser
import pyautogui
import time
import pygetwindow as gw

# Default monitor res: 2560x1440
MAX_WIDTH = 1440
MAX_HALF_HEIGHT = 1265
SLACK_PATH = r"C:\Users\joshv\AppData\Local\slack\slack.exe"
OUTLOOK_PATH = r"C:\Program Files\Microsoft Office\root\Office16\OUTLOOK.EXE"
SPOTIFY_PATH = r"C:\Users\joshv\AppData\Roaming\Spotify\Spotify.exe"
WINDOWSTERMINAL_PATH = r"C:\Users\joshv\AppData\Local\Microsoft\WindowsApps\wt.exe"
WEBSITES = {
        "github": "https://github.com/pandell/LandRiteWeb", 
        "TeamCity": "https://build.pandell.com/", 
        "LandRiteJira": "https://pandell.atlassian.net/secure/RapidBoard.jspa?rapidView=87",
        "Harvest": "https://pandell.harvestapp.com/welcome",
        "EmployeePortal": "http://employee/"}


# Manual connection click on the GlobalProtect icon and click connect
# pip install pyautogui
def connectVpn():    
    try:
        # Click tray icon
        xPosIcon = 2351
        yPosIcon = 1440-16
        pyautogui.click(xPosIcon, yPosIcon)
        time.sleep(0.5)
        # Click connect icon
        xPosConnect = 2401
        yPosConnect = 1440-77
        pyautogui.click(xPosConnect,yPosConnect)
        time.sleep(10)
        # Close tray icon
        pyautogui.click(xPosIcon, yPosIcon)
        print("Connected to GlobalProtect VPN")
    except Exception as e:
        print("Failed to connect to VPN: " + str(e))

def launchSlack():
    try:
        subprocess.Popen(SLACK_PATH)
        print("Slack started successfully.")
    except Exception as e:
        print("Failed to start slack: " + str(e))

def launchOutlook():
    try:
        subprocess.Popen(OUTLOOK_PATH)
        print("Outlook started successfully.")
    except Exception as e:
        print("Failed to start Outlook: " + str(e))

def launchChrome(websites):
    try:
        for website in websites.values():
            webbrowser.open_new_tab(website)
            print("Opened tab for: " + website)
    except Exception as e:
        print("Failed to start Chrome: " + str(e))

def launchSpotify():
    try:
        subprocess.Popen(SPOTIFY_PATH)
        print("Spotify started successfully.")
    except Exception as e:
        print("Failed to start Spotify: " + str(e))

def launchTerminal():
    try:
        subprocess.Popen(WINDOWSTERMINAL_PATH)
        print("Terminal started successfully.")
    except Exception as e:
        print("Failed to start Terminal: " + str(e))

# pip install pygetwindow
def resizeWindowRightTopVertical(windowName):
    try:
        window = gw.getWindowsWithTitle(windowName)[0]
        window.width = MAX_WIDTH
        window.height = MAX_HALF_HEIGHT
        window.right = 4000
        window.top = -298
    except Exception as e:
        print("Failed to resize and move window: " + windowName + " " + str(e))

def resizeWindowRightBottomVertical(windowName):
    try:
        window = gw.getWindowsWithTitle(windowName)[0]
        window.width = MAX_WIDTH
        window.height = MAX_HALF_HEIGHT
        window.right = 4000
        window.top = 968
    except Exception as e:
        print("Failed to resize and move window: " + windowName + " " + str(e))

def resizeWindowLeftFullVertical(windowName):
    try:
        window = gw.getWindowsWithTitle(windowName)[0]
        window.right = 0
        window.maximize()
    except Exception as e:
        print("Failed to resize and move window: " + windowName + " " + str(e))

def deleteTempAspFile(user):
    directory = r"C:\Users\joshv\AppData\Local\Temp\Temporary ASP.NET Files"

def buildRunServer():
    buildCommand = "yarn"
    runCommand = "yarn start"

def resetServer():
    resetCommand = "yarn clobber"

def main():
    connectVpn()
    launchSlack()
    launchOutlook()    
    launchChrome(WEBSITES)
    launchSpotify()
    launchTerminal()
    resizeWindowRightTopVertical("slack")
    resizeWindowRightBottomVertical("outlook")
    resizeWindowLeftFullVertical("chrome")

main()