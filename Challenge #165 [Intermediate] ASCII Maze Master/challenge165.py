"""
A solution to reddit's /r/dailyprogrammer challenge #165 found here:
http://www.reddit.com/r/dailyprogrammer/comments/278ptv/642014_challenge_165_intermediate_ascii_maze/

Author: Tom Faris <ta.faris@gmail.com>
"""
import sys

class MazeObject(object):
    
    def __init__(self, maze, x, y, token=None):
        self.maze = maze
        self.position = (x, y)
        self.token = token
        
    def can_move_to(self, obj):
        pass
    
    def can_pass(self):
        return False
        
    def is_goal(self):
        return False
        
    def __str__(self):
        return self.token or ''

class Wall(MazeObject):
    """
    Walls are impassible (go figure).
    """
    
class Room(MazeObject):
    """
    Rooms can be moved through.
    """
    def can_pass(self):
        return True
        
class Start(Room):
    """
    The maze starts here!
    """
    
class End(Room):
    """
    The maze ends here!
    """
    def is_goal(self):
        return True

tokens = {
    '#': Wall,
    ' ': Room,
    'S': Start,
    'E': End
}

class Maze(dict):
    up, right, down, left = (0, -1), (1, 0), (0, 1), (-1, 0)
    dirs = (up, right, down, left)

    def add(self, maze_obj):
        self[maze_obj.position] = maze_obj

    def get_relative(self, from_obj, direction):
        """
        Get the next maze object in the specified direction, relative
        to the specified position.
        """
        pos = (
            from_obj.position[0] + direction[0],
            from_obj.position[1] + direction[1]
        )
        # Use an impassible maze object as default if there's
        # nothing there.
        return self.get(pos, MazeObject(self, *pos))
        
    def navigate(self):
        """
        Navigate the maze. Returns a list of MazeObjects defining the
        path through the maze.
        """
        start = filter(lambda m: isinstance(m, Start), self.values())
        if start: 
            path = self._next_move(None, start[0], list())
            if path:
                path.reverse()  # Path comes back in reverse order
                return path
            else:
                print("I can't find the way out! This maze has no exit that I can get to.")
        else:
            print("I can't find the way in! This maze has no start.")
            
    def _next_move(self, last, current, path):
        """
        Determine the next move to take from the current position, as
        arrived to from the last position, and entire current path 
        list.
        """
        if current.is_goal():
            # Found the end
            path.append(current)
            return path
        else:
            for dir in Maze.dirs:
                next = self.get_relative(current, dir)
                if next != last and next.can_pass():
                    next_moves = list(path)
                    end_found = self._next_move(current, next, next_moves)
                    if isinstance(end_found, list):
                        end_found.append(current)
                        return end_found
        return False
        
    def print_maze(self, path=None):
        """
        Print the maze, optionally marking up the list of MazeObjects.
        """
        # Sort keys by row, then column, before printing
        s = ''
        w = 0
        # Don't draw path over start and end
        preserve_token = (Start, End)
        for pos in sorted(sorted(self.keys(), key=lambda p:p[0]), key=lambda p:p[1]):
            if w == self.width:
                w = 0
                s += '\n'
            r = self[pos]
            s += '*' if path and type(r) not in preserve_token and r in path else str(self[pos])
            w += 1
        print(s)

def parse_maze(filepath):
    """
    Parse the specified maze file. Returns a Maze instance.
    """
    maze = Maze()
    with open(filepath) as maze_file:    
        w, h = maze_file.next().split(' ')
        maze.width = int(w)
        maze.height = int(h)
        x, y = 0, 0
        for line in maze_file:
            for token in line.replace('\r', '').replace('\n', ''):
                maze.add(tokens.get(token, MazeObject)(maze, x, y, token=token))
                x += 1
            x = 0
            y += 1
    return maze
    
if __name__ == '__main__':
    sys.setrecursionlimit(99999)  # For big mazes...
    maze_file = sys.argv[1]
    m = parse_maze(maze_file)
    path = m.navigate()
    
    print('Maze:')
    m.print_maze()
    print('')
    print('Solved:')
    m.print_maze(path=path)
                